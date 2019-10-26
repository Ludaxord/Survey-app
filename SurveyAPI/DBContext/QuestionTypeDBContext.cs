using System;
using Microsoft.EntityFrameworkCore;
using SurveyAPI.Entities;

namespace SurveyAPI.DBContext
{
    public class QuestionTypeDBContext: DbContext
    {
        public DbSet<QuestionType> QuestionsTypes { get; set; }
        public QuestionTypeDBContext(DbContextOptions<QuestionTypeDBContext> options) : base(options) {
            Database.EnsureCreated();
        }
    }
}
