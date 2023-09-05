using FinalAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private static FinalPRN231Context context = new FinalPRN231Context();

        [HttpGet]
        public ActionResult get()
        {
            try
            {
                var data = context.Contacts.ToList();
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
                var data = context.Contacts.FirstOrDefault(s => s.ContactId == id);
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
        public ActionResult getByAccountId(int id)
        {
            try
            {
                var data = context.Contacts.ToList().Where(s => s.AccountId == id);
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
        public ActionResult Post(Contact contact)
        {
            try
            {
                context.Contacts.Add(contact);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                var data = context.Contacts.FirstOrDefault(s => s.ContactId == id);
                context.Contacts.Remove(data);
                context.SaveChanges();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
