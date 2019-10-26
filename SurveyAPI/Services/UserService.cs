using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SurveyAPI.DBContext;
using SurveyAPI.Entities;
using SurveyAPI.Helpers;
using SurveyAPI.Interfaces;

namespace SurveyAPI.Services
{
    public class UserService : IUserService
    {
        private UserDBContext _context;

        private SurveyDBContext _surveyContext;

        public UserService(UserDBContext context, SurveyDBContext surveyContext)
        {
            _context = context;
            _surveyContext = surveyContext;
        }

        public User GetById(int id)
        {
            return _context.Users.Find(id);
        }

        public User Authenticate(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password)) {
                return null;
            }

            var user = _context.Users.SingleOrDefault(x => x.Email == email);

            if (user == null) {
                return null;
            }

            if (!VerifyPassword(password, user.Password, user.PasswordSalt)) {
                return null;
            }

            return user;

        }

        public User Create(User user, string password)
        {

            var surveys = _surveyContext.Surveys;

            if (string.IsNullOrWhiteSpace(password)) {
                throw new AppException("Password is required");
            }

            if (_context.Users.Any(x => x.Email == user.Email)) {
                throw new AppException($"Email {user.Email} is already taken");
            }

            CreateHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            user.Password = passwordHash;
            user.PasswordSalt = passwordSalt;

            JArray surveysIdArray = new JArray();
            foreach (var survey in surveys) {
                surveysIdArray.Add(survey.Survey_Id);
            }
            string surveysLeftString = JsonConvert.SerializeObject(surveysIdArray);

            user.SurveysLeft = surveysLeftString;

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        private static void CreateHash(string password, out byte[] passwordHash, out byte[] passwordSalt) {
            if (password == null) {
                throw new ArgumentNullException("password");
            }
            if (string.IsNullOrWhiteSpace(password)) {
                throw new ArgumentException("value cannot be empty or contains only space");
            }

            var hmacsha = new HMACSHA512();

            passwordSalt = hmacsha.Key;
            passwordHash = hmacsha.ComputeHash(Encoding.UTF8.GetBytes(password));

        }

        private static bool VerifyPassword(string password, byte[] hash, byte[] salt) {
            if (password == null) {
                throw new ArgumentNullException("password");
            }
            if (string.IsNullOrWhiteSpace(password)) {
                throw new ArgumentException("Value cannot be empty or contains only space");
            }
            if (hash.Length != 64) {
                throw new ArgumentException("Invalid length of hash");
            }
            if (salt.Length != 128) {
                throw new ArgumentException("infalid length of salt");
            }

            var hmacsha = new HMACSHA512(salt);
            var computedHash = hmacsha.ComputeHash(Encoding.UTF8.GetBytes(password));
            for (int i = 0; i < computedHash.Length; i++) {
                if (computedHash[i] != hash[i]) {
                    return false;
                }
            }
            return true;
        }
    }
}
