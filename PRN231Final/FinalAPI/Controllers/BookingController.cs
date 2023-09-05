using FinalAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.OData.Edm;

namespace FinalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private static FinalPRN231Context context = new FinalPRN231Context();

        [HttpGet]
        public ActionResult get()
        {
            try
            {
                var data = context.Bookings.ToList();
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

        [HttpGet("ByCustomerId")]
        public ActionResult get(int id)
        {
            try
            {
                var data = context.Bookings.ToList().Where(s => s.CustomerId == id);
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
		[HttpGet("ByOrderId")]
		public ActionResult ByOrderId(int id)
		{
			try
			{
				var data = context.Bookings.FirstOrDefault(s => s.OrderId == id);
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
		[HttpGet("ByRoomIdAndCustomerIdAndSetComment")]
        public ActionResult getByRoomIdAndCustomerId(int RoomId, int CustomerId)
        {
            try
            {
                Booking data = context.Bookings.FirstOrDefault(s => s.CustomerId == CustomerId && s.RoomId==RoomId);
                if (data == null)
                {
                    return NotFound();
                }
                data.IsComment = 1;
                context.Bookings.Update(data);
                context.SaveChanges();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }       

        [HttpPost]
        public ActionResult Post(Booking booking)
        {
            try
            {
                context.Bookings.Add(booking);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPut("IsComment")]
        public ActionResult SetIsBooking(Booking booking)
        {
            try
            {
                context.Bookings.Update(booking);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpDelete]
        public ActionResult Delete(int orderId)
        {
            try
            {
                Booking booking=context.Bookings.FirstOrDefault(s => s.OrderId == orderId);
                if (booking == null)
                {
                    return NotFound();
                }
                context.Bookings.Remove(booking);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

    }
}
