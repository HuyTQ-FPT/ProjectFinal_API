using FinalAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryBookingController : ControllerBase
    {
        private static FinalPRN231Context context = new FinalPRN231Context();
    }
}
