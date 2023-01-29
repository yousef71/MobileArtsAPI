using MobileArts.api.Domain.Models;
using MobileArts.api.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using MobileArts.api.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileArts.api.Controllers
{
    [Route("/users")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllAsync()
        {
            var result= await _userService.ListAll();
            if (result == null)
                return NotFound();

            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(int id)
        {
            var result= await _userService.GetById(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("{user_name}/Logins")]
        public async Task<ActionResult<IEnumerable<User>>> GetUserWithHistory(string user_name)
        {
            var result= await _userService.GetUserWithHistory(user_name);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> Delete (int id)
        {
            var result = await _userService.Delete(id);
            if (result == null)
                return NotFound("cannot delete User");

            return Ok("User Deleted Successfully");
        }

        [HttpPost()]
        public async Task<ActionResult<User>> Add([FromBody]User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var result= await _userService.Add(user);
            if (result == null)
                return  BadRequest(new { error= "user not saved successfully." });

            return CreatedAtAction(nameof(GetById),new { id = result.UserId }, result);
        }

        [HttpPut()]
        public async Task<ActionResult<User>> Update([FromBody] User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            
            var result = await _userService.Update(user);
            if (result == null)
                return BadRequest(new { error = "user doesn't exist or not saved successfully." });

            return Ok(result);
        }

        [HttpPut("ResetPassword")]
        public async Task<ActionResult<User>> ResetPwd([FromBody] ChangePasswordRequest user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var result = await _userService.ResetPwd(user);
            if (result == null)
                return BadRequest(new { error = "user not updated successfully." });

            return Ok(result);
        }

        [HttpPut("ChangePassword")]
        public async Task<ActionResult<User>> ChangePwd([FromBody] ChangePasswordRequest user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            if (user.UserPassword == "" || user.UserPassword == null)
                return BadRequest(new { error = "UserPassword is required" });

            var result = await _userService.ChangePwd(user);
            if (result == null)
                return BadRequest(new { error = "user password not updated successfully." });

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> Login([FromBody] LogInRequest user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            if (user.UserName == "" || user.UserPassword == "")
                return BadRequest(new { error = "member name or password can't be null." });

            var result = await _userService.UserLogin(user);
            if (result == null)
                return BadRequest(new { error = "user can't login." });

            //create bearer token
            return Ok(result);
        }

    }
}
