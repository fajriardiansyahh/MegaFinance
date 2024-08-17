using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using WebAPI.DataContext;
using WebAPI.Entities;
using WebAPI.Helpers;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/v1/")]
    public class UserController : Controller
    {
        private readonly DatabaseContext _DatabaseContext;
        public UserController(DatabaseContext databaseContext)
        {
            _DatabaseContext = databaseContext;
        }

        [HttpGet]
        [Route("GetAll")]
        //[Authorize]
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
                return NotFound("Storage Location Not Found");

            return Ok(user);
        }

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
                return NotFound("Storage Location Not Found");

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
                return NotFound("Storage Location Not Found");

            _DatabaseContext.Remove(user);
            await _DatabaseContext.SaveChangesAsync();

            return Ok(await GetAll());
        }
    }
}
