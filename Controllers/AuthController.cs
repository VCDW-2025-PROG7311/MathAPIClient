using System.Text;
using Firebase.Auth;
using MathAPIClient.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MathAPIClient.Controllers
{
    public class AuthController : Controller
    {
        
        private static HttpClient httpClient = new()
        {
            BaseAddress = new Uri("http://localhost:5184/"),
        };

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(LoginModel login)
        {
            StringContent jsonContent = new(JsonConvert.SerializeObject(login), Encoding.UTF8,"application/json"); 
            HttpResponseMessage response = await httpClient.PostAsync("api/Auth/Register", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                AuthResponse? deserialisedResponse = JsonConvert.DeserializeObject<AuthResponse>(jsonResponse);
                
                HttpContext.Session.SetString("currentUser", deserialisedResponse.UserId);
                HttpContext.Session.SetString("MathJWT", deserialisedResponse.Token);
                return RedirectToAction("Calculate", "Math");                
            } else
            {
                ViewBag.Result = "An error has occurred";
                return View();
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel login)
        {
            StringContent jsonContent = new(JsonConvert.SerializeObject(login), Encoding.UTF8,"application/json"); 
            HttpResponseMessage response = await httpClient.PostAsync("api/Auth/Login", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                AuthResponse? deserialisedResponse = JsonConvert.DeserializeObject<AuthResponse>(jsonResponse);
                
                HttpContext.Session.SetString("currentUser", deserialisedResponse.UserId);
                HttpContext.Session.SetString("MathJWT", deserialisedResponse.Token);
                return RedirectToAction("Calculate", "Math");                
            } else
            {
                ViewBag.Result = response.Content.ReadAsStringAsync().Result;
                return View();
            }            
        }

        [HttpGet]
        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("currentUser");
            HttpContext.Session.Remove("JWT");
            return RedirectToAction("Login");
        }
        

    }
}