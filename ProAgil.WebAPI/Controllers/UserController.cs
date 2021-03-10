using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProAgil.Domain.Identity;
using ProAgil.WebAPI.Dtos;

namespace ProAgil.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;

        public UserController(IConfiguration config,
                              UserManager<User> userManager,
                              SignInManager<User> signInManager,
                              IMapper mapper)
        {
            _config = config;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser(UserDto userDto)
        {
            return Ok(userDto);
        }

         [HttpPost("Register")]
         [AllowAnonymous]
        public async Task<IActionResult> Register(UserDto userDto)
        {
           try
           {
               var user = _mapper.Map<User>(userDto);
               var result = await _userManager.CreateAsync(user, userDto.Password);
               var userToReturn = _mapper.Map<UserDto>(user);

               if (result.Succeeded)
               {
                   return Created("GetUser", userToReturn);
               }

               return BadRequest(result.Errors);
           }
           catch (System.Exception ex)
           {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou " + ex.Message);
           }
        }

         [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDto userlogin)
        {
           try
           {
               var user = await _userManager.FindByNameAsync(userlogin.UserName);

               var result = await _signInManager.CheckPasswordSignInAsync(user, userlogin.Password, false);

               if (result.Succeeded)
               {
                   var appUser = await _userManager.Users
                                .FirstOrDefaultAsync(u => u.NormalizedUserName == userlogin.UserName.ToUpper());

                    var userToReturn = _mapper.Map<UserLoginDto>(appUser);

                    return Ok(new {
                        token = GenerateJWToken(appUser).Result,
                        user = userToReturn
                    });
               }

               return Unauthorized();

           }
           catch (System.Exception ex)
           {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou " + ex.Message);
           }
        }

        private async Task<string> GenerateJWToken(User user)
        {
            return "";
        }
    }
}