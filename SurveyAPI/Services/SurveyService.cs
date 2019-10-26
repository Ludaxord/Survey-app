using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SurveyAPI.DBContext;
using SurveyAPI.Entities;
using SurveyAPI.Interfaces;

namespace SurveyAPI.Services
{
    public class SurveyService : ISurveyService
    {
        private SurveyDBContext _context;

        private UserDBContext _userContext;

        public SurveyService(SurveyDBContext context, UserDBContext userContext) {
            _context = context;
            _userContext = userContext;
        }

        public List<SurveyJSON> GetSurvey()
        {
            var surveys = _context.Surveys;
            List<SurveyJSON> surveyArray = new List<SurveyJSON>();
            foreach (var survey in surveys)
            {
                var surveyJSON = new SurveyJSON();
                dynamic options_json = JsonConvert.DeserializeObject(survey.Survey_Options);
                dynamic finish_user_json = JsonConvert.DeserializeObject(survey.Survey_Finished_Users);
                string json = JsonConvert.SerializeObject(options_json);
                Console.WriteLine($"Count of array: {options_json.Count}");
                surveyJSON.survey_id = survey.Survey_Id;
                surveyJSON.survey_name = survey.Survey_Name;
                surveyJSON.survey_finished_users = finish_user_json;
                surveyJSON.survey_options = options_json;
                surveyArray.Add(surveyJSON);
            }

            return surveyArray;
        }

        public SurveyResults GetSurveyResult(int survey_id, int user_id) {

            var survey = _context.Surveys.Find(survey_id);

            if (survey != null)
            {

                var results = survey.Survey_Finished_Users;

                var questions = survey.Survey_Options;

                dynamic finish_user_json = JsonConvert.DeserializeObject(results);

                dynamic option_json = JsonConvert.DeserializeObject(questions);

                JArray answers = new JArray();
                List<JSONAnswers> jAnsList = new List<JSONAnswers>();
                SurveyResults surveyResults = new SurveyResults();
                foreach (var result in finish_user_json)
                {
                    var resultUserId = result["user_id"];
                    var resultSurveyName = result["user_id"];
                    var resultSurveyId = (int)result["survey_id"];
                    if (resultUserId == user_id)
                    {
                        if (survey_id == resultSurveyId)
                        {
                            surveyResults.SurveyId = resultSurveyId;
                            surveyResults.UserId = resultUserId;
                            surveyResults.SurveyText = survey.Survey_Name;
                            answers = result["answers"];


                            foreach (var s in option_json)
                            {
                                JSONAnswers jSONAnswers = new JSONAnswers();
                                List<string> answersString = new List<string>();
                                List<string> answersOptions = new List<string>();
                                foreach (var a in answers)
                                {
                                    if (a["question_number"] == s["question_number"])
                                    {
                                        jSONAnswers.QuestionNumber = (int)s["question_number"];
                                        jSONAnswers.QuestionName = (string)s["question_text"];
                                        if (s["question_type_id"] == 6)
                                        {
                                            foreach (var iterateOption in s["question_answers"])
                                            {
                                                foreach (var i in iterateOption.Properties())
                                                {
                                                    var aq = a["question_answer"];
                                                    if (i.Name == Convert.ToString(aq))
                                                    {
                                                        answersString.Add((Convert.ToString(i.Value)));
                                                    }
                                                }
                                            }
                                            answersOptions.Add((Convert.ToString(a["question_answer"])));
                                        }
                                        else if (s["question_type_id"] == 5)
                                        {
                                            foreach (var iterateOption in s["question_answers"])
                                            {
                                                foreach (var i in iterateOption.Properties())
                                                {
                                                    var aq = a["question_answer"];
                                                    foreach (var x in aq.OfType<JProperty>())
                                                    {
                                                        if (i.Name == Convert.ToString(x.Name))
                                                        {
                                                            answersString.Add((Convert.ToString(i.Value)));
                                                        }
                                                    }
                                                }
                                            }
                                            foreach (var opt in a["question_answer"].OfType<JProperty>())
                                            {
                                                answersOptions.Add((Convert.ToString(opt.Name)));
                                            }
                                        }
                                        else
                                        {
                                            answersString.Add((Convert.ToString(a["question_answer"])));
                                        }
                                    }
                                }
                                jSONAnswers.QuestionAnswers = answersOptions;
                                jSONAnswers.QuestionAnswersText = answersString;
                                jAnsList.Add(jSONAnswers);
                            }
                        }
                    } else {
                        SurveyResults s = null;
                        return s;
                    }
                }

                surveyResults.Answers = jAnsList;

                return surveyResults;

            } else {
                SurveyResults surveyResults = null;
                return surveyResults;
            }

        }

        public List<SurveyTitle> GetTitles(int userId) {
            var surveys = _context.Surveys;
            var users = _userContext.Users.Find(userId);
            dynamic surveys_left = JsonConvert.DeserializeObject(users.SurveysLeft);
            List<SurveyTitle> arr = new List<SurveyTitle>();
            foreach (var title in surveys) {
                var titles = new SurveyTitle();
                titles.survey_id = title.Survey_Id;
                titles.survey_name = title.Survey_Name;
                if (surveys_left == null) {
                    titles.survey_finished = true;
                } else {
                    foreach (var left in surveys_left)
                    {
                        if (left == title.Survey_Id)
                        {
                            titles.survey_finished = false;
                        }
                        else
                        {
                            titles.survey_finished = true;
                        }
                    }
                }
                arr.Add(titles);
            }
            return arr;
        }
        
        public void MarkAsFinished(int userId, int surveyId) {
            var user = _userContext.Users.Find(userId);

            string surveysIdsStr = user.SurveysLeft;
            dynamic surveysIds = JsonConvert.DeserializeObject(surveysIdsStr);
            JArray removed = RemoveValue(surveysIds, surveyId);
            Console.WriteLine("MarkAsFinished");
            Console.WriteLine(surveysIds);
            Console.WriteLine(removed);
            Console.WriteLine("///////");
            string json_string = JsonConvert.SerializeObject(removed);
            user.SurveysLeft = json_string;
            _userContext.Users.Update(user);
            _userContext.SaveChanges();
        }
        
        
        public SurveyJSON GetBySurveyId(int id)
        {
            var survey = _context.Surveys.Find(id);
            var surveyJSON = new SurveyJSON();
            dynamic option_json = JsonConvert.DeserializeObject(survey.Survey_Options);
            dynamic finish_user_json = JsonConvert.DeserializeObject(survey.Survey_Finished_Users);
            surveyJSON.survey_id = survey.Survey_Id;
            surveyJSON.survey_name = survey.Survey_Name;
            surveyJSON.survey_finished_users = finish_user_json;
            surveyJSON.survey_options = option_json;
            return surveyJSON;
        }

        private static JArray RemoveValue(JArray oldArray, dynamic obj)
        {
            List<int> temp2 = oldArray.ToObject<List<int>>();
            temp2.Remove(obj);
            return JArray.FromObject(temp2);
        }

        public void AddUserToSurveyFinishedUsersColumn(int userId, int surveyId, JObject json)
        {
            var survey = _context.Surveys.Find(surveyId);
            if (survey == null) {
                throw new Exception("Survey not found");
            }
            MarkAsFinished(userId, surveyId);
            dynamic finish_user_json = JsonConvert.DeserializeObject(survey.Survey_Finished_Users);
            var array = finish_user_json;
            finish_user_json.Add(json);
            string json_string = JsonConvert.SerializeObject(finish_user_json);
            Console.WriteLine(finish_user_json);
            Console.WriteLine(json_string);
            survey.Survey_Finished_Users = json_string;
            _context.Surveys.Update(survey);
            _context.SaveChanges();
        }
    }
}
