using System;
namespace SurveyAPI.Entities
{
    public class SurveyTitle
    {
        public int survey_id { get; set; }
        public string survey_name { get; set; }
        public bool survey_finished { get; set; }
    }
}
