using FinalAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;

namespace FinalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomDetailController : ControllerBase
    {
        private static FinalPRN231Context context = new FinalPRN231Context();

        [HttpGet("ByRoomId")]
        public ActionResult get(int id)
        {
            try
            {
                var data = context.RoomDetails.Include(s => s.Room).FirstOrDefault(s => s.RoomId == id);
                if (data == null)
                {
                    return NotFound();
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [EnableQuery]
        public ActionResult getAll()
        {
            try
            {
                var data = context.RoomDetails.AsQueryable();
                if (data == null)
                {
                    return NotFound();
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
