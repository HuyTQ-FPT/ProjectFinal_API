using FinalAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;

namespace FinalMVC.Controllers
{
    public class CommentController : Controller
    {
		public async Task<IActionResult> Index(int? id)
		{
            RoomDetail room = new RoomDetail();
            String link = "http://localhost:5285/api/RoomDetail/ByRoomId?id=" + id;
			using (HttpClient httpClient = new HttpClient())
            {
                using (HttpResponseMessage res = await httpClient.GetAsync(link))
                {
                    using (HttpContent content = res.Content)
                    {
                        String data = await content.ReadAsStringAsync();
                        room = JsonConvert.DeserializeObject<RoomDetail>(data);
                    }
                }
            }
            ViewBag.GetRoom = room;
            ViewBag.RoomId = id;
            ViewBag.AccountId = AccountIDLogin();
            return View();
		}
        public async Task<IActionResult> Admin()
        {
            HttpContext.Session.SetString("title", "Comment");
            return View();
        }
        private int AccountIDLogin()
        {
            String dataLogin = HttpContext.Session.GetString("Login");
            Account account = JsonConvert.DeserializeObject<Account>(dataLogin);
            return account.AccountId;
        }
        public async Task<IActionResult> Comment(Comment comment)
        {
            String id = Request.Form["RoomID"];
            
            //comment.DateComment=DateNow;
            int cusId = await GetCustomerIDLogin();
            String link = "http://localhost:5285/api/Comment";
            String linkGetBooking = "http://localhost:5285/api/Booking/ByRoomIdAndCustomerIdAndSetComment?RoomId=" + id+"&CustomerId="+ cusId;

            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpResponseMessage res = await httpClient.PostAsJsonAsync(link, comment))
                {
                }
                using (HttpResponseMessage res = await httpClient.GetAsync(linkGetBooking))
                {

                }
            }
            return Redirect($"/HistoryBooking");
        }
        private async Task<int> GetCustomerIDLogin()
        {
            String dataLogin = HttpContext.Session.GetString("Login");
            Account account = JsonConvert.DeserializeObject<Account>(dataLogin);
            Customer customer = new Customer();
            String link = "http://localhost:5285/api/Customer/ByAccountID?id=" + account.AccountId;
            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpResponseMessage res = await httpClient.GetAsync(link))
                {
                    using (HttpContent content = res.Content)
                    {
                        String data = await content.ReadAsStringAsync();
                        customer = JsonConvert.DeserializeObject<Customer>(data);
                    }
                }
            }
            return customer.CustomerId;
        }
        public async Task<IActionResult> Delete(int id)
        {
            String link ="http://localhost:5285/api/Comment";
            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpResponseMessage res = await httpClient.DeleteAsync(link))
                {
                }
            }
            return Redirect($"/Publisher");
        }
    }
}
