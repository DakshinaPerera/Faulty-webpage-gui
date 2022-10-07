using DataWebApi.Controllers;
using DataWebApi.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace BuissnessWebApi.Controllers
{
    [RoutePrefix("api/Generate")]
    public class GenerateController : ApiController
    {
        private BankDetailsController detailsController = new BankDetailsController();

        private readonly Random rand = new Random();

        private readonly string[] fullNameList = { "James", "Mary", "Robert", "Patricia", "John", "Jennifer", "Michael", "Linda", "David", "Elizabeth", "William", "Barbara", "Richard", "Susan", "Joseph", "Jessica" };

        private readonly string[] lastlNameList = { "Smith", "Johnson", "Williams", "Brown", "Jones", "Miller", "Davis", "Garcia", "Rodriguez", "Wilson", "Martinez", "Anderson", "Taylor", "Thomas", "Hernandez", "Moore" };
        // POST: api/Generate
        
        [ResponseType(typeof(BankDetail))]
        public IHttpActionResult PostGenerate([FromBody]int id)
        {
            IHttpActionResult val = BadRequest(ModelState); 
            for (int i = id; i <= 100 ; i++)
            {
                BankDetail bankDetail = new BankDetail();
                bankDetail.Id = i;  
                bankDetail.firstName = fullNameList[rand.Next(fullNameList.Length)];
                bankDetail.lastName = lastlNameList[rand.Next(lastlNameList.Length)];
                bankDetail.acctNo = rand.Next(000000000, 999999999);
                bankDetail.pin = rand.Next(99999);
                bankDetail.balance = rand.Next(0, 99999999);
                RestClient restClient = new RestClient("http://localhost:64415/");
                RestRequest restRequest = new RestRequest("api/BankDetails",Method.Post);
                restRequest.AddJsonBody((bankDetail));
                RestResponse restResponse = restClient.Execute(restRequest);
                val = (IHttpActionResult)restResponse;
                

            }
            return val;
        }


    }
}
