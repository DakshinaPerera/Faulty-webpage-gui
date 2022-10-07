using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DataWebApi.Models;

namespace DataWebApi.Controllers
{
    public class GenerateController : ApiController
    {
        private readonly Random rand = new Random();
        private BankDataBaseEntities db = new BankDataBaseEntities();

        private readonly string[] fullNameList = { "James", "Mary", "Robert", "Patricia", "John", "Jennifer", "Michael", "Linda", "David", "Elizabeth", "William", "Barbara", "Richard", "Susan", "Joseph", "Jessica" };

        private readonly string[] lastlNameList = { "Smith", "Johnson", "Williams", "Brown", "Jones", "Miller", "Davis", "Garcia", "Rodriguez", "Wilson", "Martinez", "Anderson", "Taylor", "Thomas", "Hernandez", "Moore" };
        // POST: api/Generate

        [ResponseType(typeof(BankDetail))]
        public IHttpActionResult PostGenerate(int id)
        {
            IHttpActionResult val = BadRequest(ModelState);
            for (int i = id; i <= 100; i++)
            {
                BankDetail bankDetail = new BankDetail();
                bankDetail.Id = i;
                bankDetail.firstName = fullNameList[rand.Next(fullNameList.Length)];
                bankDetail.lastName = lastlNameList[rand.Next(lastlNameList.Length)];
                bankDetail.acctNo = rand.Next(000000000, 999999999);
                bankDetail.pin = rand.Next(99999);
                bankDetail.balance = rand.Next(0, 99999999);
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                db.BankDetails.Add(bankDetail);

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    if (BankDetailExists(bankDetail.Id))
                    {
                        return Conflict();
                    }
                    else
                    {
                        throw;
                    }
                }
                val =  CreatedAtRoute("DefaultApi", new { id = bankDetail.Id }, bankDetail);


            }
            return Ok(val);
            
        }
        private bool BankDetailExists(int id)
        {
            return db.BankDetails.Count(e => e.Id == id) > 0;
        }
    }
}
