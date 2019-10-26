using System;
using Microsoft.EntityFrameworkCore;
using SurveyAPI.Entities;

namespace SurveyAPI.DBContext
{
	public class UserDBContext : DbContext
    {
        public UserDBContext(DbContextOptions<UserDBContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
     }

}
