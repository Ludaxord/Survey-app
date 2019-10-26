using System;
using SurveyAPI.Entities;

namespace SurveyAPI.Interfaces
{
    public interface IUserService
    {
        User Authenticate(string email, string password);
        User Create(User user, string password);
        User GetById(int id);
    }
}
