using System;
using System.Collections.Generic;
using System.Linq;
using SurveyAPI.DBContext;
using SurveyAPI.Entities;

namespace SurveyAPI.Extensions
{
    public static class UserDBContextExtensions
    {

        public static void CreateSeedData(this UserDBContext context)
        {
            if (context.Users.Any())
                return;
            var users = new List<User>()
               {
                //new User()
                //    {
                //    Email = "konrad.uciechowski@gmail.com",
                //    FirstName = "Konrad",
                //    LastName = "Uciechowski",
                //}
               };
            context.AddRange(users);
            context.SaveChanges();
        }
    }
}
