using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using url_shortener.Models;

namespace url_shortener.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet, Route("/")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost, Route("/")]
        public IActionResult PostUrl([FromBody] string url)
        {
            try
            {
                // If the URL does not contain HTTP prefix it with it
                if (!url.Contains("http"))
                {
                    url = "http://" + url;
                }

                // check if the shortened URL already exists within our database
                if (new LiteDB.LiteDatabase("Data/Urls.db").GetCollection<SpatacoliUrl>().Exists(u => u.ShortenedUrl == url))
                {
                    Response.StatusCode = 405;
                    return Json(new UrlResponse()
                    {
                        url = url,
                        status = "Already shortened",
                        token = null
                    });
                }

                // Shorten the URL and return the token as a json string
                Shortener shortUrl = new Shortener(url);
                return Json(shortUrl.Token);
            } 
            catch (Exception ex)
            {
                if (ex.Message == "URL already exists")
                {
                    Response.StatusCode = 400;
                    return Json(new UrlResponse()
                    {
                        url = url,
                        status = "URL already exists",
                        token = new LiteDB.LiteDatabase("Data/Urls.db").GetCollection<SpatacoliUrl>().Find(u => u.Url == url).FirstOrDefault().Token
                    });
                }

                throw new Exception(ex.Message);
            }
        }

        [HttpGet, Route("/{token}")]
        public IActionResult SpatacoliRedirect([FromRoute] string token)
        {
            return Redirect(
                new LiteDB.LiteDatabase("Data/Urls.db")
                .GetCollection<SpatacoliUrl>()
                .FindOne(u => u.Token == token).Url
                );
        }

        private string FindRedirect(string url)
        {
            string result = string.Empty;

            using (var client = new HttpClient())
            {
                var response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    result = response.Headers.Location.ToString();
                }
            }

            return result;
        }
    }
}
