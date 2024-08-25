using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Thibeault.Example.Api.Helper;
using Thibeault.Example.Api.Models.Input;

namespace Thibeault.Example.Api.Controllers
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
        [HttpPost("Register")]
        [Authorize]
        public async Task<IActionResult> CreateAsync(CreateUser userDto)
        {
            // todo System.InvalidOperationException: No route matches the supplied values?
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

        [HttpDelete("DeleteUser")]
        [Authorize]
        public async Task<IActionResult> DeleteUserAsync(string userName)
        {
            ObjectResult result;

            try
            {
                IdentityUser user = await userManager.FindByNameAsync(userName);

                if (user != null)
                {
                    IdentityResult success = await userManager.DeleteAsync(user);

                    if (success.Succeeded)
                    {
                        result = Ok("User Deleted");
                    }
                    else
                    {
                        result = BadRequest("Unknown error");
                    }
                }
                else
                {
                    result = BadRequest("User not found");
                }
            }
            catch (Exception ex)
            {
                result = BadRequest(ex.Message);
            }

            return result;
        }

        [HttpPut("UpdateUser")]
        [Authorize]
        public async Task<IActionResult> UpdateUserAsync(string userName, Login userToUpdate)
        {
            ObjectResult result;

            try
            {
                IdentityUser user = await userManager.FindByNameAsync(userName);

                if (user != null)
                {
                    // todo Sometimes succeeds in deleting and returns not succeeded
                    IdentityResult success = await userManager.DeleteAsync(user);

                    if (success.Succeeded)
                    {
                        IdentityUser createUser = new()
                        {
                            UserName = userToUpdate.UserName,
                        };

                        success = await userManager.CreateAsync(createUser, userToUpdate.PassWord);

                        if (success.Succeeded)
                        {
                            result = Ok("User updated");
                        }
                        else
                        {
                            result = BadRequest("Unknown error");
                        }
                    }
                    else
                    {
                        result = BadRequest("Unknown error");
                    }
                }
                else
                {
                    result = BadRequest("User not found");
                }
            }
            catch (Exception ex)
            {
                result = BadRequest(ex.Message);
            }

            return result;
        }
    }
}