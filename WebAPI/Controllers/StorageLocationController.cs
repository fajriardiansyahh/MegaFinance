using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.DataContext;
using WebAPI.Entities;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/v1/")]
    public class StorageLocationController : Controller
    {
        private readonly DatabaseContext _DatabaseContext;
        public StorageLocationController(DatabaseContext databaseContext)
        {
            _DatabaseContext = databaseContext;
        }

        [HttpGet]
        [Route("GetAll")]
        //[Authorize]
        public async Task<IActionResult> GetAll()
        {
            var storageLocation = await _DatabaseContext.StorageLocations.ToListAsync();

            return Ok(storageLocation);
        }

        [HttpGet]
        [Route("GetByID")]
        public async Task<IActionResult> GetByID(string location_id)
        {
            var storageLocation = await _DatabaseContext.StorageLocations.FindAsync(location_id);
            if (storageLocation is null)
                return NotFound("Storage Location Not Found");

            return Ok(storageLocation);
        }

        [HttpPost]
        [Route("AddStorageLocation")]
        public async Task<ActionResult<List<StorageLocation>>> AddStorageLocation(StorageLocation storageLocation)
        {
            _DatabaseContext.StorageLocations.Add(storageLocation);
            await _DatabaseContext.SaveChangesAsync();

            return Ok(await GetAll());
        }

        [HttpPut]
        [Route("UpdateStorageLocation")]
        public async Task<IActionResult> UpdateStorageLocation(StorageLocation storageLocation)
        {
            var _storageLocation = await _DatabaseContext.StorageLocations.FindAsync(storageLocation.location_id);
            if (_storageLocation is null)
                return NotFound("Storage Location Not Found");

            _storageLocation.location_id = storageLocation.location_id;
            _storageLocation.location_name = storageLocation.location_name;

            await _DatabaseContext.SaveChangesAsync();

            return Ok(_storageLocation);
        }

        [HttpDelete]
        [Route("DeleteByID")]
        public async Task<ActionResult<List<StorageLocation>>> DeleteByID(string location_id)
        {
            var storageLocation = await _DatabaseContext.StorageLocations.FindAsync(location_id);
            if (storageLocation is null)
                return NotFound("Storage Location Not Found");

            _DatabaseContext.Remove(storageLocation);
            await _DatabaseContext.SaveChangesAsync();

            return Ok(await GetAll());
        }
    }
}
