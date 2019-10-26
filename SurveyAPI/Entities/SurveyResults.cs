using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace SurveyAPI.Entities
{
    public class SurveyResults
    {
        public SurveyResults()
        {
        }

        public int SurveyId { get; set; }
        public int UserId { get; set; }
        public string SurveyText { get; set; }
        public List<JSONAnswers> Answers { get; set; }
    }
}
