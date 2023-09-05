using FinalAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FinalMVC.Controllers
{
    public class HistoryBookingController : Controller
    {
        public async Task<IActionResult> Index()
        {
            int CustomerID = await GetCustomerIDLogin();

			List<Booking> booking = new List<Booking>();
            

            String linkHistoryBookingByCustomerId = "http://localhost:5285/api/Booking/ByCustomerId?id="+CustomerID;
            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpResponseMessage res = await httpClient.GetAsync(linkHistoryBookingByCustomerId))
                {
                    using (HttpContent content = res.Content)
                    {
                        String data = await content.ReadAsStringAsync();
                        booking = JsonConvert.DeserializeObject<List<Booking>>(data);
                    }
                }
            }
            if(booking.Count > 0)
            {
				HashSet<int?> listRoomIdBooking = new HashSet<int?>();
				foreach (Booking item in booking)
				{
					listRoomIdBooking.Add(item.RoomId);
				}
                foreach (Booking B in booking)
                {
                    using (HttpClient httpClient = new HttpClient())
                    {
                        foreach (int? item in listRoomIdBooking)
                        {
                            String linkroom = "http://localhost:5285/api/Room/byId?id=" + item;
                            using (HttpResponseMessage res = await httpClient.GetAsync(linkroom))
                            {
                                using (HttpContent content = res.Content)
                                {
                                    String data = await content.ReadAsStringAsync();
                                    Room roomTB = JsonConvert.DeserializeObject<Room>(data);
                                    if (roomTB != null)
                                    {
                                        String linkGetCateById = "http://localhost:5285/api/Category/id?id=" + roomTB.CagogoryId;
                                        using (HttpResponseMessage res1 = await httpClient.GetAsync(linkGetCateById))
                                        {
                                            using (HttpContent content1 = res1.Content)
                                            {
                                                String data1 = await content1.ReadAsStringAsync();
                                                Category cate = JsonConvert.DeserializeObject<Category>(data1);
                                                if (cate != null)
                                                {
                                                    roomTB.Cagogory = cate;
                                                }
                                            }
                                        }
                                        if (roomTB.RoomId == B.RoomId)
                                        {
                                            B.Room = roomTB;
                                        }                                       
                                    }
                                }
                            }
                        }
                    }
                }               
			}
            String dataLogin = HttpContext.Session.GetString("Login");
            Account account = JsonConvert.DeserializeObject<Account>(dataLogin);

            ViewBag.GetBooking = booking;
            ViewBag.Report = account.IsReport;

			HttpContext.Session.SetString("title", "HistoryBooking");
			return View();
        }

		private async Task<int> GetCustomerIDLogin()
		{
			String dataLogin = HttpContext.Session.GetString("Login");
			Account account = JsonConvert.DeserializeObject<Account>(dataLogin);
			Customer customer = new Customer();
			String link = "http://localhost:5285/api/Customer/ByAccountID?id="+account.AccountId;
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
