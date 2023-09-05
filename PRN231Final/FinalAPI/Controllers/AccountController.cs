using FinalAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
		private static FinalPRN231Context context = new FinalPRN231Context();

		[HttpGet("CheckAccount")]
        public ActionResult get(String username, String password)
        {
            try
            {
                var data = context.Accounts.FirstOrDefault(s=>s.Password.Equals(password) && s.Username.Equals(username));
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
        [HttpGet("Report")]
        public ActionResult Report(int AccountId)
        {
            try
            {
                Account data = context.Accounts.FirstOrDefault(s => s.AccountId==AccountId);
                if (data == null)
                {
                    return NotFound();
                }
                data.IsReport = 1;
                context.Update(data);
                context.SaveChanges();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpGet("UnReport")]
        public ActionResult UnReport(int AccountId)
        {
            try
            {
                Account data = context.Accounts.FirstOrDefault(s => s.AccountId == AccountId);
                if (data == null)
                {
                    return NotFound();
                }
                data.IsReport = 0;
                context.Update(data);
                context.SaveChanges();
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
                var data = context.Accounts.ToList().FirstOrDefault(s => s.AccountId == id);
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
        [HttpGet("ListAccountReport")]
        public ActionResult ListReport()
        {
            try
            {
                var data = context.Accounts.ToList().Where(s => s.IsReport == 1);
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
        public ActionResult Post(Account account) 
        {
            try
            {                
                if (account == null)
                {
                    return NotFound();
                }
                context.Accounts.Add(account);
                context.SaveChanges();
                return Ok(account);
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
