using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using SurveyAPI.Entities;

namespace SurveyAPI.Interfaces
{
    public interface ISurveyService
    {
        List<SurveyJSON> GetSurvey();
        List<SurveyTitle> GetTitles(int userId);
        SurveyJSON GetBySurveyId(int id);
        void AddUserToSurveyFinishedUsersColumn(int userId, int surveyId, JObject json);
        void MarkAsFinished(int userId, int surveyId);
        SurveyResults GetSurveyResult(int survey_id, int user_id);
    }
}
