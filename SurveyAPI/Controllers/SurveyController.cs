using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SurveyAPI.DBContext;
using Newtonsoft.Json;
using SurveyAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using SurveyAPI.Interfaces;
using Newtonsoft.Json.Linq;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SurveyAPI.Controllers
{
    [Route("api/[controller]")]
    public class SurveyController : MainController
    {

        private ISurveyService _surveyService;

        public SurveyController(ISurveyService surveyService) {
            _surveyService = surveyService;
        }

        [Authorize]
        [HttpGet("[action]/{userId}")]
        public IActionResult GetTitles(int userId) {
            var arr = _surveyService.GetTitles(userId);
            return Ok(arr);
        }

        [Authorize]
        [HttpGet("[action]")]
        public IActionResult GetSurvey() {
            var surveyArray = _surveyService.GetSurvey();
            return Ok(surveyArray);
        }
        
        [Authorize]
        [HttpGet("[action]/{id}")]
        public IActionResult GetBySurveyId(int id) {
            var survey = _surveyService.GetBySurveyId(id);
            return Ok(survey);
        }

        [Authorize]
        [HttpGet("[action]/{survey_id}/{user_id}")]
        public IActionResult GetSurveyResult(int survey_id, int user_id) {
            var answers = _surveyService.GetSurveyResult(survey_id, user_id);
            if (answers != null) {
                return Ok(answers);
            } else {
                return Ok(new { message = "Sorry, You need to complete survey first" });
            }
        }

        [Authorize]
        [HttpPost("[action]/{surveyId}/{userId}")]
        public IActionResult PostFinishedSurvey(int surveyId, int userId, [FromBody]FinishedSurvey finished) {

            var id = finished.Id;
            var user = finished.UserId;

            if (id != surveyId) {
                return BadRequest(new { message = "wrong Id" });
            }

            if (user != userId) {
                return BadRequest(new { message = "wrong user" });
            }

            var questionsJson = finished.Questions;
            JObject newFinishedSurvey = new JObject
            {
                ["user_id"] = userId,
                ["survey_id"] = surveyId,
                ["answers"] = questionsJson
            };

            //foreach (JObject q in questionsJson) {
            //    var questionNumber = q["question_number"];
            //    var questionAnswer = q["question_answer"];
            //    var questionTypeId = q["question_type_id"];
            //    int TypeIdVal = (int)(questionTypeId ?? 0);
            //    if (TypeIdVal == 5) {
            //        foreach (var answer in questionAnswer) {
            //            Console.WriteLine(answer);
            //        }
            //    } else {
            //        Console.WriteLine(questionAnswer);
            //    }

            //}

            _surveyService.AddUserToSurveyFinishedUsersColumn(userId, surveyId, newFinishedSurvey);

            return Ok(new {message = "thank you for taking part in survey"});
        }
    }
}
