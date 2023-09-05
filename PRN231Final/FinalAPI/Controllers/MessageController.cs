using FinalAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private static FinalPRN231Context context = new FinalPRN231Context();

        [HttpGet]
        public ActionResult get()
        {
            try
            {
                var data = context.Messages.ToList();
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

        [HttpGet("id")]
        public ActionResult get(int id)
        {
            try
            {
                var data = context.Messages.ToList().FirstOrDefault(s => s.MessageId == id);
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
        public ActionResult Post(Message message)
        {
            try
            {
                context.Messages.Add(message);
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
