using FinalAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OData.Edm;
using Newtonsoft.Json;
using System;

namespace FinalMVC.Controllers
{
    public class ContactController : Controller
    {
        public async Task<IActionResult> Index()
        {
			HttpContext.Session.SetString("title", "Contact");
			return View();
        }
        public async Task<IActionResult> Admin()
        {
            HttpContext.Session.SetString("title", "Contact");
            return View();
        }

        public async Task<IActionResult> AddContact(Contact contact)
		{
            DateTime dateTime = DateTime.Now;
            contact.Date = dateTime.ToString("dd/MM/yyyy");
            contact.AccountId = AccountIDLogin();
            contact.IsAdmin = 0;
			String link = "http://localhost:5285/api/Contact";
			using (HttpClient httpClient = new HttpClient())
			{
				using (HttpResponseMessage res = await httpClient.PostAsJsonAsync(link, contact))
				{
                    AddNoti(contact.Title, contact.Message);
				}
			}
			return Redirect($"/Contact");
		}
        private async void AddNoti(String title,String mess)
        {
            DateTime dateTime = DateTime.Now;
            Notification notification=new Notification();
            notification.Title = "Contact của bạn: "+title;
            notification.Message= mess;
            notification.IsNew = 1;
            notification.AccountId= AccountIDLogin();
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
        private int AccountIDLogin() {
            String dataLogin = HttpContext.Session.GetString("Login");
			Account account = JsonConvert.DeserializeObject<Account>(dataLogin);
            return account.AccountId;
		}
		public async Task<IActionResult> getAll()
        {
            Category categorie = new Category();
            String link = "http://localhost:5033/api/Category";
            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpResponseMessage res = await httpClient.GetAsync(link))
                {
                    using (HttpContent content = res.Content)
                    {
                        String data = await content.ReadAsStringAsync();
                        List<Category> categories = JsonConvert.DeserializeObject<List<Category>>(data);
                    }
                }
            }
            return View(categorie);
        }
        public async Task<IActionResult> GetById(int id)
        {
            Category categorie = new Category();
            String link = "http://localhost:5033/api/Category/id?id=" + id;
            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpResponseMessage res = await httpClient.GetAsync(link))
                {
                    using (HttpContent content = res.Content)
                    {
                        String data = await content.ReadAsStringAsync();
                        Category categories = JsonConvert.DeserializeObject<Category>(data);
                    }
                }
            }
            return View(categorie);
        }
    }
}
