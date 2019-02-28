﻿using BPOSolution.Models;
using BPOSolution.Services;
using BPOSolution.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace BPOSolution.Controllers
{
    [Produces("application/json")]
    [Route("Auth")]
    public class AuthController : Controller
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;

        public AuthController(IAuthRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        [HttpPost("Registration")]
        public async Task<IActionResult> Register([FromBody] AdminRegistrationViewModel registerVM)
        {
            try
            {
                if (await _repo.UserExists(registerVM.Username))
                {
                    ModelState.AddModelError("Username", "Username already exists.");
                }

                if (await _repo.EmailExists(registerVM.Email))
                {
                    ModelState.AddModelError("Email", "Email already exists.");
                }

                if (!ModelState.IsValid)
                {
                    return Json(BadRequest(ModelState));
                }
                
                registerVM.Username = registerVM.Username.ToLower();
                registerVM.Email = registerVM.Email.ToLower();

                var adminToCreate = new Admin
                {
                    Username = registerVM.Username,
                    Email = registerVM.Email
                };

                await _repo.Register(adminToCreate, registerVM.Password);

                return Json(StatusCode(201));
            }
            catch (Exception ex)
            {
                return Json(BadRequest(ex));
            }           
        } 

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AdminLoginViewModel loginVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(StatusCode(400));
                    
                }
                var adminFromRepo = await _repo.Login(loginVM.Username, loginVM.Password);
                if(adminFromRepo == null)
                {
                    return Json(StatusCode(401));
                }
                var tokenHandler = new JwtSecurityTokenHandler();

                var key = Encoding.ASCII.GetBytes(_config.GetSection("AppSettings:Token").Value); 
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, adminFromRepo.AdminId.ToString()),
                        new Claim(ClaimTypes.Name, adminFromRepo.Username)
                    }),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha512Signature)   
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);
                
                return Json(StatusCode(200, tokenString));
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}