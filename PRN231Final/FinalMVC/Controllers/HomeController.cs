using FinalAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FinalMVC.Controllers
{
	public class HomeController : Controller
	{
		public async Task<IActionResult> Index()
		{
			List<Category> categorie = new List<Category>();
			String link = "http://localhost:5285/api/Category";
			using (HttpClient httpClient = new HttpClient())
			{
				using (HttpResponseMessage res = await httpClient.GetAsync(link))
				{
					using (HttpContent content = res.Content)
					{
						String data = await content.ReadAsStringAsync();
						categorie = JsonConvert.DeserializeObject<List<Category>>(data);
					}
				}
			}
			ViewBag.Category = categorie;
			HttpContext.Session.SetString("title", "Home");
			return View();
		}
		public async Task<IActionResult> Chat()
		{
			//ViewBag.AccountID = AccountIDLogin();
			return View();
		}
		public async Task<IActionResult> Admin()
		{
            HttpContext.Session.SetString("title", "Home");
            return View();
		}
        private int AccountIDLogin()
        {
            String dataLogin = HttpContext.Session.GetString("Login");
            Account account = JsonConvert.DeserializeObject<Account>(dataLogin);
            return account.AccountId;
        }
    }
}
