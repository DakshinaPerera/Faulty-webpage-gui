using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using WebPageGui.Models;

namespace WebPageGui.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Home";
            RestClient restClient = new RestClient("http://localhost:64415/");
            RestRequest restRequest = new RestRequest("api/BankDetails/", Method.Get);
            RestResponse restResponse = restClient.Execute(restRequest);
            List<BankDetail> bank = JsonConvert.DeserializeObject<List<BankDetail>>(restResponse.Content);

            return View(bank);
            
        }
    }
}
