using System;
using System.Collections.Generic;
using SurveyAPI.DBContext;
using SurveyAPI.Entities;
using System.Linq;

namespace SurveyAPI.Extensions
{
    public static class QuestionTypeDBContextExtensions
    {

        public static void CreateSeedData(this QuestionTypeDBContext context)
        {
            if (context.QuestionsTypes.Any())
                return;
            var questionsType = new List<QuestionType>()
               {
                new QuestionType()
                    {
                    Question_Type_Name = "number",
                    },
                new QuestionType()
                    {
                    Question_Type_Name = "multiple_option",
                    },
                new QuestionType()
                    {
                    Question_Type_Name = "single_option",
                    },
                new QuestionType()
                    {
                    Question_Type_Name = "text",
                    },
               };
            context.AddRange(questionsType);
            context.SaveChanges();
        }
    }
}

