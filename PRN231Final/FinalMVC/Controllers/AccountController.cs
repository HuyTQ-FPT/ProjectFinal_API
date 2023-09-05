using FinalAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.Json.Nodes;

namespace FinalMVC.Controllers
{
    public class AccountController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        public async Task<IActionResult> LogOut()
        {
            HttpContext.Session.Remove("Login");
            return Redirect($"/Account");
        }

        public async Task<IActionResult> Regist(Account account)
        {
            String rePassword = Request.Form["rePassword"];
            if (rePassword.Equals(account.Password))
            {
                account.RoleId = 1;
                String link = "http://localhost:5285/api/Account";
                using (HttpClient httpClient = new HttpClient())
                {
                    using (HttpResponseMessage res = await httpClient.PostAsJsonAsync(link, account))
                    {
                        using (HttpContent content = res.Content)
                        {
                        }
                    }
                }
                return Redirect($"/Account");
            }
            return Redirect($"/Account/Create");
        }
        public async Task<IActionResult> CheckAccount(Account account)
        {
            Account checkAccount= new Account();
            String link = "http://localhost:5285/api/Account/CheckAccount?username="+account.Username+"&password="+account.Password;
            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpResponseMessage res = await httpClient.GetAsync(link))
                {
                    using (HttpContent content = res.Content)
                    {
                        String data = await content.ReadAsStringAsync();
                        checkAccount = JsonConvert.DeserializeObject<Account>(data);
                        if (checkAccount.RoleId== 1)
                        {
                            HttpContext.Session.SetString("Login", data);
                            return Redirect($"/Home");
                        }else if(checkAccount.RoleId == 3)
                        {
							HttpContext.Session.SetString("Login", data);
							return Redirect($"/Home/Admin");
						}
                    }
                }
            }
            return Redirect($"/Account");
        }

        public async Task<IActionResult> Add(Category category)
        {
            String link = "http://localhost:5033/api/Category";
            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpResponseMessage res = await httpClient.PostAsJsonAsync(link, category))
                {
                    if (res.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Success");
                    }
                }
            }
            return Redirect($"/Publisher");
        }
        public async Task<IActionResult> Update(Category category)
        {
            String link = "http://localhost:5033/api/Category/id?id=" + category.CategoryId;
            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpResponseMessage res = await httpClient.PutAsJsonAsync(link, category))
                {
                    if (res.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Success");
                    }
                }
            }
            return Redirect($"/Publisher");
        }
        public async Task<IActionResult> Delete(int id)
        {
            String link = "http://localhost:5033/api/Category/id?id=" + id;
            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpResponseMessage res = await httpClient.DeleteAsync(link))
                {
                    if (res.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Success");
                    }
                }
            }
            return Redirect($"/Publisher");
        }
    }
}
