using System;
using System.Collections.Generic;
using SurveyAPI.DBContext;
using SurveyAPI.Entities;
using System.Linq;

namespace SurveyAPI.Extensions
{
    public static class SurveyDBContextExtensions
    {

        public static void CreateSeedData(this SurveyDBContext context)
        {
            if (context.Surveys.Any())
                return;
            var surveys = new List<Survey>()
               {
                new Survey()
                    {
                         Survey_Name = "Survey",
                         Survey_Options = "[]",
                        Survey_Finished_Users = "[]",
                    }
               };
            context.AddRange(surveys);
            context.SaveChanges();
        }
    }
}
