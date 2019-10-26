using System;
using Newtonsoft.Json.Linq;

namespace SurveyAPI.Entities
{
    public class FinishedSurvey
    {
        public FinishedSurvey()
        {
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public JArray Questions { get; set; }
    }
}
