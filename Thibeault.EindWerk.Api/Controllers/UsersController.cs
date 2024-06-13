using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Thibeault.EindWerk.Api.Helper;
using Thibeault.EindWerk.Api.Models.Input;
using Thibeault.EindWerk.Objects;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Thibeault.EindWerk.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly JwtHelper jwtHelper;

        public UsersController(UserManager<IdentityUser> userManager, JwtHelper jwtHelper)
        {
            this.userManager = userManager;
            this.jwtHelper = jwtHelper;
        }

        //registreren van user 
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateUser userDto)
        {
            if (!ModelState.IsValid && userDto.Password == userDto.PasswordConfirm)
            {
                return BadRequest(ModelState);
            }

            var result = await userManager.CreateAsync(new IdentityUser()
            {
                UserName = userDto.UserName,
            }, userDto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            userDto.Password = "";

            return CreatedAtAction("GetUser", new { username = userDto.UserName }, userDto);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(Login authDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await userManager.FindByNameAsync(authDto.UserName);

            if (user == null)
            {
                return BadRequest("Invalid credentials");
            }

            var isPasswordValid = await userManager.CheckPasswordAsync(user, authDto.PassWord);

            if (!isPasswordValid)
            {
                return BadRequest("Invalid credentials");
            }

            var token = jwtHelper.CreateToken(user);

            HttpContext.Response.Cookies.Append("access_token", token.Token, new CookieOptions { HttpOnly = true });

            //generate JWT TOKEN
            return Ok(token);

        }

    }
}
