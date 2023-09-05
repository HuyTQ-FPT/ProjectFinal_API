using FinalAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace FinalMVC.Controllers
{
    public class RoomController : Controller
    {
        public async Task<IActionResult> Index()
        {			
            RoomTimeOutBooking();
            String linkRoom;
            String linkCate;

            List<Category> categorie = new List<Category>();
            List<Room> rooms = new List<Room>();
            
            if (!String.IsNullOrEmpty(Request.Query["Category"]))
            {
				int cate = int.Parse(Request.Query["Category"]);
				linkRoom = "http://localhost:5285/api/Room/NotYetBookingByCategory?cate=" + cate;
			}
			else {
				linkRoom = "http://localhost:5285/api/Room/NotYetBooking";
			}			 
			 linkCate = "http://localhost:5285/api/Category";
			using (HttpClient httpClient = new HttpClient())
			{
				using (HttpResponseMessage res = await httpClient.GetAsync(linkRoom))
				{
					using (HttpContent content = res.Content)
					{
						String data = await content.ReadAsStringAsync();
						rooms = JsonConvert.DeserializeObject<List<Room>>(data);
					}
				}
				using (HttpResponseMessage res = await httpClient.GetAsync(linkCate))
				{
					using (HttpContent content = res.Content)
					{
						String data = await content.ReadAsStringAsync();
						categorie = JsonConvert.DeserializeObject<List<Category>>(data);
					}
				}				
			}
            ViewBag.GetRoom= rooms;
			ViewBag.GetCategory = categorie;
			
			HttpContext.Session.SetString("title", "Room");
			return View();
        }
		private async void RoomTimeOutBooking()
        {
            String linkRoomIsBooking = "http://localhost:5285/api/Room/IsBooking";
            List<Room> RoomIsBooking = new List<Room>();
            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpResponseMessage res = await httpClient.GetAsync(linkRoomIsBooking))
                {
                    using (HttpContent content = res.Content)
                    {
                        String data = await content.ReadAsStringAsync();
                        RoomIsBooking = JsonConvert.DeserializeObject<List<Room>>(data);
                    }
                }
            }
            if(RoomIsBooking.Count > 0)
            {
				foreach (Room item in RoomIsBooking)
				{
					String key = item.RoomId.ToString();
					String value = Request.Cookies[key];
					if (String.IsNullOrEmpty(value))
					{
                        String link = "http://localhost:5285/api/Room/ResetRoom?RoomId=" + int.Parse(key);
                        Room room = new Room();
						using (HttpClient httpClient = new HttpClient())
						{
                            using (HttpResponseMessage res = await httpClient.GetAsync(link))
                            {
                                RemoveCookie(key);
                            }
                        }
					}
				}
			}                      
        }
        private void RemoveCookie(string key)
        {
			String value = "";
            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Append(key, value, cookieOptions);
        }
    
        public async Task<IActionResult> Search()
        {
            String Name = Request.Form["Name"];
            String Price = Request.Form["Price"];
            String Children = Request.Form["Children"];
            String Adult = Request.Form["Adult"];

			String linkRoom = "http://localhost:5285/api/Room?$filter=";
			String linkRoomDetail = "http://localhost:5285/api/RoomDetail?$filter=";
			String linkCate = "http://localhost:5285/api/Category";

			List<Category> categorie = new List<Category>();
			List<Room> rooms = new List<Room>();
			List<RoomDetail> roomDetails = new List<RoomDetail>();

			
			if (!String.IsNullOrEmpty(Name) && !String.IsNullOrEmpty(Price))
			{
                String[] price = Price.Split("-");
                linkRoom += "contains(Name, '"+Name+"') and (Price gt " + price[0] +" and Price le " + price[1] + ") and (IsBooking eq 0)";
			}
            else if (!String.IsNullOrEmpty(Name))
            {
                linkRoom += "contains(Name, '"+Name+ "') and (IsBooking eq 0)";
            }
            else if(!String.IsNullOrEmpty(Price))
			{
                String[] price = Price.Split("-");
				if(price.Length > 1) {
					linkRoom += "(Price gt " + price[0] + " and Price le " + price[1] + ") and (IsBooking eq 0)";
				}
				else
				{
					linkRoom += "(Price gt " + price[0] + ") and (IsBooking eq 0)";
				}
                
			}
			else
			{
                linkRoom= "http://localhost:5285/api/Room";

            }
			using (HttpClient httpClient = new HttpClient())
			{
				using (HttpResponseMessage res = await httpClient.GetAsync(linkRoom))
				{
					using (HttpContent content = res.Content)
					{
						String data = await content.ReadAsStringAsync();
						rooms = JsonConvert.DeserializeObject<List<Room>>(data);
					}
				}
				using (HttpResponseMessage res = await httpClient.GetAsync(linkCate))
				{
					using (HttpContent content = res.Content)
					{
						String data = await content.ReadAsStringAsync();
						categorie = JsonConvert.DeserializeObject<List<Category>>(data);
					}
				}
			}

			if (!String.IsNullOrEmpty(Children) && !String.IsNullOrEmpty(Adult))
			{
				linkRoomDetail += "children eq " + Children + " and adult eq " + Adult;
			}
			else if (!String.IsNullOrEmpty(Children))
			{
				linkRoomDetail += "children eq " + Children;
			}
			else if (!String.IsNullOrEmpty(Adult))
			{
				linkRoomDetail += "adult eq " + Adult;
			}
			else
			{
				linkRoomDetail = "http://localhost:5285/api/RoomDetail";
			}
			if(!String.IsNullOrEmpty(Adult) || !String.IsNullOrEmpty(Children))
			{
                using (HttpClient httpClient = new HttpClient())
                {
                    using (HttpResponseMessage res = await httpClient.GetAsync(linkRoomDetail))
                    {
                        using (HttpContent content = res.Content)
                        {
                            String data = await content.ReadAsStringAsync();
                            roomDetails = JsonConvert.DeserializeObject<List<RoomDetail>>(data);
                        }
                    }
                }
            }       
            List<Room> Search = new List<Room>();
			if (roomDetails.Count > 0)
			{
				foreach(Room room in rooms)
				{
					foreach(RoomDetail roomDetail in roomDetails)
					{
						if (room.RoomId == roomDetail.RoomId)
						{
							Search.Add(room);
							break;
						}
					}
				}
			}
			else
			{
				Search.AddRange(rooms);
			}
					
			ViewBag.GetCategory = categorie;
			ViewBag.GetRoom = Search;
			ViewBag.Name = Name;
			ViewBag.Price = Price;
			ViewBag.Children = Children;
			ViewBag.Adult = Adult;

            return View();
		}
    }
}
