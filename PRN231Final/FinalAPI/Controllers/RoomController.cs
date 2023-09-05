using FinalAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;

namespace FinalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private FinalPRN231Context context = new FinalPRN231Context();
        [HttpGet]
        [EnableQuery]
        public ActionResult getAll()
        {
            try
            {
                var data = context.Rooms.AsQueryable();
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

        [HttpGet("ResetRoom")]
        public ActionResult ResetRoom(int RoomId)
        {
            try
            {
                Room data = context.Rooms.FirstOrDefault(s => s.RoomId == RoomId);
                if (data == null)
                {
                    return NotFound();
                }
                data.IsBooking = 0;
                context.Rooms.Update(data);
                context.SaveChanges();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
		[HttpGet("SetIsBooking")]
		public ActionResult SetIsBooking(int RoomId)
		{
			try
			{
				Room data = context.Rooms.FirstOrDefault(s => s.RoomId == RoomId);
				if (data == null)
				{
					return NotFound();
				}
				data.IsBooking = 1;
				context.Rooms.Update(data);
				context.SaveChanges();
				return Ok(data);
			}
			catch (Exception ex)
			{
				return BadRequest();
			}
		}

		[HttpGet("IsBooking")]
        public ActionResult getRoomIsBooking()
        {
            try
            {
                var data = context.Rooms.ToList().Where(s => s.IsBooking == 1);
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

        [HttpGet("NotYetBooking")]
        public ActionResult get()
        {
            try
            {
                var data = context.Rooms.ToList().Where(s => s.IsBooking == 0);
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

        [HttpGet("NotYetBookingByCategory")]
        public ActionResult getByCategory(int cate)
        {
            try
            {
                var data = context.Rooms.ToList().Where(s => s.IsBooking == 0 && s.CagogoryId==cate);
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

		[HttpGet("byId")]
        public ActionResult get(int id)
        {
            try
            {
                var data = context.Rooms.ToList().FirstOrDefault(s => s.RoomId == id);
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
