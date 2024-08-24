using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using WebAPI.DataContext;
using WebAPI.Entities.Users;
using WebAPI.Helpers;

namespace WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]/")]
    public class UserController : Controller
    {
        private readonly DatabaseContext _DatabaseContext;
        public UserController(DatabaseContext databaseContext)
        {
            _DatabaseContext = databaseContext;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var user = await _DatabaseContext.Users.ToListAsync();

            return Ok(user);
        }

        [HttpGet]
        [Route("GetByID")]
        public async Task<IActionResult> GetByID(int user_id)
        {
            var user = await _DatabaseContext.Users.FindAsync(user_id);
            if (user is null)
                return NotFound("Users Not Found");

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("AddUser")]
        public async Task<ActionResult<List<User>>> AddUser(User user)
        {
            user.password = SecurePasswordHasher.Hash(user.password);
            _DatabaseContext.Users.Add(user);
            await _DatabaseContext.SaveChangesAsync();

            return Ok(await GetAll());
        }

        [HttpPut]
        [Route("UpdateUser")]
        public async Task<IActionResult> UpdateUser(User user)
        {
            var _user = await _DatabaseContext.Users.FindAsync(user.user_id);
            if (_user is null)
                return NotFound("Users Not Found");

            _user.user_name = user.user_name;
            _user.is_active = user.is_active;

            await _DatabaseContext.SaveChangesAsync();

            return Ok(_user);
        }

        [HttpDelete]
        [Route("DeleteByID")]
        public async Task<ActionResult<List<User>>> DeleteByID(int user_id)
        {
            var user = await _DatabaseContext.Users.FindAsync(user_id);
            if (user is null)
                return NotFound("Users Not Found");

            _DatabaseContext.Remove(user);
            await _DatabaseContext.SaveChangesAsync();

            return Ok(await GetAll());
        }
    }
}
