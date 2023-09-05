using FinalAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FinalMVC.Controllers
{
    public class MessageController : Controller
    {
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
