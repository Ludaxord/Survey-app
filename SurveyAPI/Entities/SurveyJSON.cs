using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace SurveyAPI.Entities
{
    public class SurveyJSON
    {
        public SurveyJSON()
        {
        }

        public int survey_id { get; set; }
        public string survey_name { get; set; }
        public JArray survey_options { get; set; }
        public JArray survey_finished_users { get; set; }
    }
}
