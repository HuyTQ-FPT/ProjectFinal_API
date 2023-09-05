using FinalAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using System.Collections;

namespace FinalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private static FinalPRN231Context context = new FinalPRN231Context();

        [HttpGet]
        [EnableQuery]
        public ActionResult get()
        {
            try
            {
                var data = context.Notifications.AsQueryable();
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

        [HttpGet("ByAccountId")]
        public ActionResult get(int id)
        {
            try
            {
                var data = context.Notifications.ToList().Where(s => s.AccountId == id);
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
        [HttpPost]
        public ActionResult Post(Notification notification)
        {
            try
            {
                context.Notifications.Add(notification);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpDelete]
		public ActionResult Delete(int id)
		{
			try
			{
				Notification data = context.Notifications.FirstOrDefault(s => s.NotificationId == id);
				if (data == null)
				{
					return NotFound();
				}
				context.Notifications.Remove(data);
				context.SaveChanges();
				return Ok(data);
			}
			catch (Exception ex)
			{
				return BadRequest();
			}
		}
		[HttpPost("DeleteAll")]
		public ActionResult DeleteAll(List<int> id)
		{
			try
			{
                List<Notification> List = new List<Notification>();
                foreach(int item in id)
                {
                    Notification data = context.Notifications.FirstOrDefault(s => s.NotificationId == item);
                    if (data == null)
                    {
                        return NotFound();
                    }
                    List.Add(data);
                }				
				
				context.Notifications.RemoveRange(List);
				context.SaveChanges();
				return Ok(List);
			}
			catch (Exception ex)
			{
				return BadRequest();
			}
		}
	}
}
