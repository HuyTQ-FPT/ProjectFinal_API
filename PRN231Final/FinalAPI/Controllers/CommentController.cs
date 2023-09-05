using FinalAPI.DTO;
using AutoMapper;
using FinalAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;

namespace FinalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private static FinalPRN231Context context = new FinalPRN231Context();
        private MapperConfiguration config;
        private IMapper mapper;
        public CommentController()
        {
            config = new MapperConfiguration(cfg => cfg.AddProfile(new MapperProfile()));
            mapper = config.CreateMapper();
        }
        [HttpGet]
        public ActionResult get()
        {
            try
            {
                var data = context.Comments.Include(s => s.Account)
                            .Select(e => new
                            {
                                CommentId = e.CommentId,
                                CommentContent = e.CommentContent,
                                AccountId = e.AccountId,
                                Start = e.Start,
                                DateComment=e.DateComment,
                                RoomId = e.RoomId,
                                Account = new Account()
                                {
                                    Username = e.Account.Username,
                                    Password = e.Account.Password,
                                    RoleId = e.Account.RoleId,
                                    IsReport = e.Account.IsReport
                                }
                            }).ToList();
                //List<Comment> data = context.Comments.ToList();
                if (data == null)
                {
                    return NotFound();
                }
                //List<CommentDTO> DTO = data.Select(p => mapper.Map<Comment, CommentDTO>(p)).ToList();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpGet("Up")]
        public ActionResult Sortup()
        {
            try
            {
                List<Comment> data = context.Comments.Include(s => s.Account).OrderBy(s => s.Start).ToList();
                //List<Comment> data = context.Comments.ToList();
                if (data == null)
                {
                    return NotFound();
                }
                List<CommentDTO> DTO = data.Select(p => mapper.Map<Comment, CommentDTO>(p)).ToList();
                return Ok(DTO);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpGet("Down")]
        public ActionResult SortDown()
        {
            try
            {
                List<Comment> data = context.Comments.Include(s => s.Account).OrderByDescending(s => s.Start).ToList();
                //List<Comment> data = context.Comments.ToList();
                if (data == null)
                {
                    return NotFound();
                }
                List<CommentDTO> DTO = data.Select(p => mapper.Map<Comment, CommentDTO>(p)).ToList();
                return Ok(DTO);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpGet("ByAccountId")]
        public ActionResult GetByAccountId(int id)
        {
            try
            {
                List<Comment> data = context.Comments.Include(s => s.Account).Where(s => s.AccountId == id).ToList();
                //List<Comment> data = context.Comments.ToList();
                if (data == null)
                {
                    return NotFound();
                }
                List<CommentDTO> DTO = data.Select(p => mapper.Map<Comment, CommentDTO>(p)).ToList();
                return Ok(DTO);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpGet("ByRoomId")]
        public ActionResult getByRoomId(int RoomId)
        {
            try
            {
                List<Comment> data = context.Comments.Include(s => s.Account).Where(s => s.RoomId == RoomId).ToList();
                //List<Comment> data = context.Comments.ToList();
                if (data == null)
                {
                    return NotFound();
                }
                List<CommentDTO> DTO = data.Select(p => mapper.Map<Comment, CommentDTO>(p)).ToList();
                return Ok(DTO);
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
                var data = context.Comments.ToList().FirstOrDefault(s => s.CommentId == id);
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
        public ActionResult Post(Comment comment)
        {
            try
            {
                context.Comments.Add(comment);
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
                var data = context.Comments.FirstOrDefault(s => s.CommentId == id);
                context.Comments.Remove(data);
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
