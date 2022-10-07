using DataWebApi.Controllers;
using DataWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace BuissnessWebApi.Controllers
{
    public class BankController : ApiController
    {
        private BankDetailsController detailsController = new BankDetailsController();
        // GET: api/Bank
        public IQueryable<BankDetail> GetBank()
        {
            
            return detailsController.GetBankDetails();
        }

        // GET: api/Bank/5
        [ResponseType(typeof(BankDetail))]
        public IHttpActionResult GetBank(int id)
        {
            return detailsController.GetBankDetail(id);
        }

        // POST: api/Bank
        [ResponseType(typeof(BankDetail))]
        public IHttpActionResult PostBank(BankDetail bankDetail)
        {
           return detailsController.PostBankDetail(bankDetail);
        }

        // PUT: api/Bank/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBank(int id, BankDetail bankDetail)
        {
            return detailsController.PutBankDetail(id, bankDetail);
        }

        // DELETE: api/Bank/5
        [ResponseType(typeof(BankDetail))]
        public IHttpActionResult DeleteBank(int id)
        {
            return detailsController.DeleteBankDetail(id);
        }

    }
}
