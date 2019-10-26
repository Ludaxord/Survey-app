using System;
using System.Collections.Generic;
using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SurveyAPI.Entities;
using SurveyAPI.Helpers;
using SurveyAPI.Interfaces;
using SurveyAPI.Services;
using Microsoft.Extensions.Options;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SurveyAPI.Controllers
{
    [Route("api/user/[controller]")]
    public class AuthController : MainController
    {
        private IUserService _userService;

        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public AuthController(IUserService userService, IMapper mapper, IOptions<AppSettings> appSettings) {
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpGet("[action]")]
        public IActionResult MainPageMessage() {
            return Ok(new {message = "Hello welcome to survey app, to fill questionaire register/login. Then choose survey."});
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public IActionResult Login([FromBody]UserBinder userParameters) {
            var user = _userService.Authenticate(userParameters.Email, userParameters.Password);

            if (user == null) {
                return BadRequest(new { message = "Error: Incorrect password or email" });
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_appSettings.Secret);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescription);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new {
                user.Id,
                user.Email,
                user.FirstName,
                user.LastName,
                Token = tokenString
            });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _userService.GetById(id);
            var userBinder = _mapper.Map<UserBinder>(user);
            return Ok(userBinder);
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public IActionResult Register([FromBody]UserBinder userParameters) {
            var user = _mapper.Map<User>(userParameters);

            try {
                var subject = "Register in Survey App";
                var first_last_name = $"{userParameters.FirstName} {userParameters.LastName}";
                var text = $"Hello {userParameters.FirstName} {userParameters.LastName}, Thank you for register in Survey App. Login and go to Survey List to look at your Surveys.";

                new EmailClient().SendSMTPEmails(userParameters.Email, subject, text, first_last_name);

                _userService.Create(user, userParameters.Password);

                return Ok(new {message = "Your account has been created!"});
            } catch(AppException exception) {
                return BadRequest(new { message = exception.Message });
            }
        }

        [Authorize]
        [HttpGet("[action]")]
        public IActionResult WelcomeMessage() {
            var message = new MessageJSON();
            message.success = true;
            message.message = "Hello, thank you for login to survey app. Bellow you can find ";
            return Ok(message);
        }
    }
}
