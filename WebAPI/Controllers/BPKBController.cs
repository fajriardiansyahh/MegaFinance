using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.DataContext;
using WebAPI.Entities;

namespace WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]/")]
    public class BPKBController : Controller
    {
        private readonly DatabaseContext _DatabaseContext;
        public BPKBController(DatabaseContext databaseContext)
        {
            _DatabaseContext = databaseContext;
        }

        [HttpGet]
        [Route("GetAll")]
        //[Authorize]
        public async Task<IActionResult> GetAll()
        {
            var BPKB = await _DatabaseContext.BPKB.ToListAsync();

            return Ok(BPKB);
        }

        [HttpGet]
        [Route("GetByID")]
        public async Task<IActionResult> GetByID(string agreement_number)
        {
            var BPKB = _DatabaseContext.BPKB.Where(bpkb => bpkb.agreement_number == agreement_number).FirstOrDefault();
            if (BPKB is null)
                return NotFound("Storage Location Not Found");

            return Ok(BPKB);
        }

        [HttpPost]
        [Route("AddBPKB")]
        public async Task<ActionResult<List<BPKB>>> AddBPKB(BPKB BPKB)
        {
            BPKB.created_on = DateTime.Now;

            _DatabaseContext.BPKB.Add(BPKB);
            await _DatabaseContext.SaveChangesAsync();

            return Ok(await GetAll());
        }

        [HttpPut]
        [Route("UpdateBPKB")]
        public async Task<IActionResult> UpdateBPKB(BPKB BPKB)
        {
            var _BPKB = _DatabaseContext.BPKB.Where(bpkb => bpkb.agreement_number == BPKB.agreement_number).FirstOrDefault();
            if (_BPKB is null)
                return NotFound("Storage Location Not Found");

            _BPKB.bpkb_no = BPKB.bpkb_no;
            _BPKB.branch_id = BPKB.branch_id;
            _BPKB.bpkb_date = BPKB.bpkb_date;
            _BPKB.faktur_no = BPKB.faktur_no;
            _BPKB.faktur_date = BPKB.faktur_date;
            _BPKB.location_id = BPKB.location_id;
            _BPKB.police_no = BPKB.police_no;
            _BPKB.bpkb_date_in = BPKB.bpkb_date_in;
            _BPKB.last_updated_on = DateTime.Now;

            await _DatabaseContext.SaveChangesAsync();

            return Ok(_BPKB);
        }

        [HttpDelete]
        [Route("DeleteByID")]
        public async Task<ActionResult<List<BPKB>>> DeleteByID(string agreement_number)
        {
            var BPKB = await _DatabaseContext.BPKB.FindAsync(agreement_number);
            if (BPKB is null)
                return NotFound("Storage Location Not Found");

            _DatabaseContext.Remove(BPKB);
            await _DatabaseContext.SaveChangesAsync();

            return Ok(await GetAll());
        }
    }
}
