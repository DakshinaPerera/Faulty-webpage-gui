using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using WebPageGui.Models;

namespace WebPageGui.Controllers
{
    public class BankController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Bank";
            return View();
        }

        [HttpGet]
        public IActionResult Search(int id)
        {
            RestClient restClient = new RestClient("http://localhost:64415/");
            RestRequest restRequest = new RestRequest("api/BankDetails/{id}", Method.Get);
            restRequest.AddUrlSegment("id", id);
            RestResponse restResponse = restClient.Execute(restRequest);
            return Ok(restResponse.Content);
        }
    }

}
