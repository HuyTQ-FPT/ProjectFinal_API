using FinalAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;

namespace FinalMVC.Controllers
{
    public class BookingController : Controller
    {
        public async Task<IActionResult> Index()
        {
			if (!CheckLogin())
			{
				return Redirect($"/Account");
			}
			int id = int.Parse(Request.Query["id"]);

            RoomDetail roomDetail = new RoomDetail();
            List<Comment> comment = new List<Comment>();
            int NumberComment = 0 ;

            String linkComment = "http://localhost:5285/api/Comment/ByRoomId?RoomId="+id;
            String linkGetRoom = "http://localhost:5285/api/RoomDetail/ByRoomId?id=" + id;

            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpResponseMessage res = await httpClient.GetAsync(linkComment))
                {
                    using (HttpContent content = res.Content)
                    {
                        String data = await content.ReadAsStringAsync();
                        comment = JsonConvert.DeserializeObject<List<Comment>>(data);
                    }
                }
                using (HttpResponseMessage res = await httpClient.GetAsync(linkGetRoom))
                {
                    using (HttpContent content = res.Content)
                    {
                        String data = await content.ReadAsStringAsync();
                        roomDetail = JsonConvert.DeserializeObject<RoomDetail>(data);
                    }
                }				
			}
            int cusID = await GetCustomerIDLogin();
            NumberComment = comment.Count;
            ViewBag.GetComment = comment;
            ViewBag.GetRoom = roomDetail;
            ViewBag.NumberComment = NumberComment;
            ViewBag.CustomerID = cusID;

            HttpContext.Session.SetString("title", "Booking");
            return View();
        }

        public async Task<IActionResult> Booking(int? id)
        {
            if (!CheckLogin()) {
				return Redirect($"/Account");
			}
            
            String? checkin = Request.Form["checkIn"];
            String? checkOut = Request.Form["checkOut"];
            String DateNow = DateTime.Now.ToString("yyyy-MM-dd");

			if (String.Compare(checkin, checkOut) > 0 || String.Compare(DateNow,checkin) >0)
			{
				return Redirect($"/Booking?id="+id);
			}

			//DateTime date1 = Convert.ToDateTime(checkin);
			DateTime date2 = Convert.ToDateTime(checkOut);
            DateTime dateNow = Convert.ToDateTime(DateNow);
            TimeSpan time = date2.Subtract(dateNow);

            String DeadlineRoom = time.Days.ToString();

			String link = "http://localhost:5285/api/Room/SetIsBooking?RoomId=" + id;
            using (HttpClient httpClient = new HttpClient())
            {
				using (HttpResponseMessage res = await httpClient.GetAsync(link))
				{
					CreateCookie(id.ToString(), DeadlineRoom);
                    AddNoti();
				}
            }
            return Redirect($"/Room");
        }
        private async void AddNoti()
        {
            DateTime dateTime = DateTime.Now;
            Notification notification = new Notification();
            notification.Title = "Thư Book Room";
            notification.Message = "Bạn đã đặt phòng thành công";
            notification.IsNew = 1;
            notification.AccountId = AccountIDLogin();
            //notification.Date = dateTime.ToString("dd/MM/yyyy");
            notification.Date = dateTime.ToString("d/M/yyyy");
            String link = "http://localhost:5285/api/Notification";
            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpResponseMessage res = await httpClient.PostAsJsonAsync(link, notification))
                {
                }
            }
        }
        private int AccountIDLogin()
        {
            String dataLogin = HttpContext.Session.GetString("Login");
            Account account = JsonConvert.DeserializeObject<Account>(dataLogin);
            return account.AccountId;
        }
        public async Task<IActionResult> Cancelbooking(int id)
        {
            Booking booking = new Booking();
            
            String linkRemoveBooking = "http://localhost:5285/api/Booking?orderId="+ id;
            String linkGetBookingByOrderId = "http://localhost:5285/api/Booking/ByOrderId?id=" + id;

            using (HttpClient httpClient = new HttpClient())
            {
				using (HttpResponseMessage res = await httpClient.GetAsync(linkGetBookingByOrderId))
				{
                    using(HttpContent content = res.Content)
                    {
						String data = await content.ReadAsStringAsync();
						booking = JsonConvert.DeserializeObject<Booking>(data);
					}
				}
				String link = "http://localhost:5285/api/Room/ResetRoom?RoomId=" + booking.RoomId;
				using (HttpResponseMessage res = await httpClient.GetAsync(link))
                {
                    RemoveCookie(booking.RoomId.ToString());
                }
                using (HttpResponseMessage res = await httpClient.DeleteAsync(linkRemoveBooking))
                {
                }
            }
            return Redirect($"/HistoryBooking");
        }

        private void RemoveCookie(string key)
        {
            String value = "";
            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Append(key, value, cookieOptions);
        }

        private void CreateCookie(string key, string value)
        {
            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddDays(int.Parse(value));
            Response.Cookies.Append(key,value,cookieOptions);
        }
        
        private Boolean CheckLogin()
        {
			String dataLogin = HttpContext.Session.GetString("Login");
            if (String.IsNullOrEmpty(dataLogin)){
                return false;
            }
            return true;
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
    }
}
