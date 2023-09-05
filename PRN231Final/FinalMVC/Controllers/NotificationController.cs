using FinalAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FinalMVC.Controllers
{
	public class NotificationController : Controller
	{
		public async Task<IActionResult> Index()
		{
			int idAccount = AccountIDLogin();
			List<Notification> notifications = new List<Notification>();
			String link = "http://localhost:5285/api/Notification/ByAccountId?id=" + idAccount;

			using (HttpClient httpClient = new HttpClient())
			{
				using (HttpResponseMessage res = await httpClient.GetAsync(link))
				{
					using (HttpContent content = res.Content)
					{
						String data = await content.ReadAsStringAsync();
						notifications = JsonConvert.DeserializeObject<List<Notification>>(data);
					}
				}
			}

			ViewBag.GetNotification = notifications;

			HttpContext.Session.SetString("title", "Notification");
			return View();
		}
		private int AccountIDLogin()
		{
			String dataLogin = HttpContext.Session.GetString("Login");
			Account account = JsonConvert.DeserializeObject<Account>(dataLogin);
			return account.AccountId;
		}
		public async Task<IActionResult> Search()
		{
			String from = Request.Form["From"];
			String to = Request.Form["To"];
			if(!String.IsNullOrEmpty(from) && !String.IsNullOrEmpty(to))
			{
                String[] GETYEARMONTHDAYFROM = from.Split("-");
                String[] GETYEARMONTHDAYTO = to.Split("-");
                int Fday = 0;
                int Tday = 0;
                int Fmonth = 0;
                int Tmonth = 0;

                if (GETYEARMONTHDAYFROM[2].StartsWith("0"))
                {
                    Fday = int.Parse(GETYEARMONTHDAYFROM[2].Substring(1));
                }
                else
                {
                    Fday = int.Parse(GETYEARMONTHDAYFROM[2]);
                }
                if (GETYEARMONTHDAYFROM[1].StartsWith("0"))
                {
                    Fmonth = int.Parse(GETYEARMONTHDAYFROM[1].Substring(1));
                }
                else
                {
                    Fmonth = int.Parse(GETYEARMONTHDAYFROM[1]);
                }
                //int Fyear = int.Parse(GETYEARMONTHDAYFROM[0].Trim());

                if (GETYEARMONTHDAYTO[2].StartsWith("0"))
                {
                    Tday = int.Parse(GETYEARMONTHDAYTO[2].Substring(1));
                }
                else
                {
                    Tday = int.Parse(GETYEARMONTHDAYTO[2]);
                }

                if (GETYEARMONTHDAYTO[1].StartsWith("0"))
                {
                    Tmonth = int.Parse(GETYEARMONTHDAYTO[1].Substring(1));
                }
                else
                {
                    Tmonth = int.Parse(GETYEARMONTHDAYTO[1]);
                }
                //int Tyear = int.Parse(GETYEARMONTHDAYTO[0].Trim());

                if (Fmonth > Tmonth || Fday > Tday)
                {
                    return Redirect($"/Notification");
                }

                int idAccount = AccountIDLogin();

                List<Notification> notifications = new List<Notification>();
                List<Notification> Search = new List<Notification>();

                String link = "http://localhost:5285/api/Notification/ByAccountId?id=" + idAccount;

                using (HttpClient httpClient = new HttpClient())
                {
                    using (HttpResponseMessage res = await httpClient.GetAsync(link))
                    {
                        using (HttpContent content = res.Content)
                        {
                            String data = await content.ReadAsStringAsync();
                            notifications = JsonConvert.DeserializeObject<List<Notification>>(data);
                        }
                    }
                }
                foreach (Notification noti in notifications)
                {
                    String[] DATE = noti.Date.Split("/");
                    if ((Fmonth <= int.Parse(DATE[1]) && int.Parse(DATE[1]) <= Tmonth)
                        && (Fday <= int.Parse(DATE[0]) && int.Parse(DATE[0]) <= Tday))
                    {
                        Search.Add(noti);
                    }
                }

                ViewBag.GetNotification = Search;
                ViewBag.From = from;
                ViewBag.To = to;
            }
            else
            {
                return Redirect($"/Notification");
            }
			
			return View();
		}
        public async Task<IActionResult> Task3()
        {
            List<int> listId = new List<int>();
            String value = Request.Form["task3"];
            String[] id=value.Split("/");
            for(int i = 0; i < id.Length-1; i++)
            {
                listId.Add(int.Parse(id[i]));
            }
            String link = "http://localhost:5285/api/Notification/DeleteAll";
            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpResponseMessage res = await httpClient.PostAsJsonAsync(link,listId))
                {
                    using (HttpContent content = res.Content)
                    {
                    }
                }
            }
                
            return Redirect($"/Notification");
        }

    }
}
