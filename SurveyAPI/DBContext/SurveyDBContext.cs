using System;
using Microsoft.EntityFrameworkCore;
using SurveyAPI.Entities;

namespace SurveyAPI.DBContext
{
    public class SurveyDBContext : DbContext
    {
        public DbSet<Survey> Surveys { get; set; }
        public SurveyDBContext(DbContextOptions<SurveyDBContext> options) : base(options) {
            Database.EnsureCreated();
        }
    }
}
