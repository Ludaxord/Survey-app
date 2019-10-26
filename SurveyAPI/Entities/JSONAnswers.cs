using System;
using System.Collections.Generic;

namespace SurveyAPI.Entities
{
    public class JSONAnswers
    {
        public JSONAnswers()
        {
        }

        public int QuestionNumber { get; set; }
        public string QuestionName { get; set; }
        public List<string> QuestionAnswers { get; set; }
        public List<string> QuestionAnswersText { get; set; }
    }
}
