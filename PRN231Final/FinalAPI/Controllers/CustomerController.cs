using FinalAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private static FinalPRN231Context context = new FinalPRN231Context();

        [HttpGet]
        public ActionResult get()
        {
            try
            {
                var data = context.Customers.ToList();
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

        [HttpGet("ByAccountID")]
        public ActionResult get(int id)
        {
            try
            {
                var data = context.Customers.ToList().FirstOrDefault(s => s.AccountId == id);
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
        public ActionResult Post(Customer customer)
        {
            try
            {
                context.Customers.Add(customer);
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
