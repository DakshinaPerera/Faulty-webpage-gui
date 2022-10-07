using DataWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BuissnessWebApi.Controllers
{
    [RoutePrefix("api/Search")]
    public class SearchController : ApiController
    {
        private BankDataBaseEntities db = new BankDataBaseEntities();


        // GET: api/Search/5
        [Route("{searchText}")]
        public IHttpActionResult Get(string searchText)
        {
            string[] nameList = searchText.Split(' ');
            string searchFname = nameList[0];
            string searchLname = nameList[1];
            int id= 1;
            BankDetail bankDetail = db.BankDetails.Find(id);
            while (bankDetail != null)
            {
                if ((searchFname.ToLower().Contains(bankDetail.firstName)) && (searchLname.ToLower().Contains(bankDetail.lastName))){
                    return Ok(bankDetail);
                    break;
                }
                id++;
                bankDetail = db.BankDetails.Find(id);
            }
            return NotFound();
        }


    }
}
