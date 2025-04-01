using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MathAPIClient.Models;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Headers;

namespace MathAPIClient.Controllers
{
    public class MathController : Controller
    {

        private static HttpClient httpClient = new()
        {
            BaseAddress = new Uri("http://localhost:5184/"),
        };

        public IActionResult Calculate()
        {
            var token = HttpContext.Session.GetString("MathJWT");

            if (token == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            List<SelectListItem> operations = new List<SelectListItem> {
                new SelectListItem { Value = "1", Text = "+" },
                new SelectListItem { Value = "2", Text = "-" },
                new SelectListItem { Value = "3", Text = "*" },
                new SelectListItem { Value = "4", Text = "/" },
            };
            ViewBag.Operations = operations;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Calculate(decimal? FirstNumber, decimal? SecondNumber,int Operation)
        {
            var token = HttpContext.Session.GetString("MathJWT");
            if (token == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var currentUser = HttpContext.Session.GetString("currentUser");
            decimal? Result = 0;
            MathCalculation mathCalculation;

            try
            {
                mathCalculation = MathCalculation.Create(FirstNumber, SecondNumber, Operation, Result, currentUser);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
                throw;
            }
            
            StringContent jsonContent = new(JsonConvert.SerializeObject(mathCalculation), Encoding.UTF8,"application/json"); 
            
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await httpClient.PostAsync("api/Math/PostCalculate", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                MathCalculation? deserialisedResponse = JsonConvert.DeserializeObject<MathCalculation>(jsonResponse);
                ViewBag.Result = deserialisedResponse.Result;

            List<SelectListItem> operations = new List<SelectListItem> {
                new SelectListItem { Value = "1", Text = "+" },
                new SelectListItem { Value = "2", Text = "-" },
                new SelectListItem { Value = "3", Text = "*" },
                new SelectListItem { Value = "4", Text = "/" },
            };
            ViewBag.Operations = operations;

            return View();
            } else
            {
                ViewBag.Result = "An error has occurred";
                return View();
            }
        }

        public async Task<IActionResult> History()
        {
            var token = HttpContext.Session.GetString("MathJWT");
            if (token == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await httpClient.GetAsync("/api/Math/GetHistory");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                List<MathCalculation>? deserialisedResponse = JsonConvert.DeserializeObject<List<MathCalculation>>(jsonResponse);
                if (deserialisedResponse.Count == 0)
                {
                    ViewBag.HistoryMessage = "No history exists";
                }
                return View(deserialisedResponse);
            }  else
            {
                ViewBag.HistoryMessage = "No history to show";
                return View();
            }            
        }

        public async Task<IActionResult> Clear()
        {
            var token = HttpContext.Session.GetString("MathJWT");
            if (token == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await httpClient.DeleteAsync("/api/Math/DeleteHistory");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
            }
            return RedirectToAction("History");
        }
    }
}