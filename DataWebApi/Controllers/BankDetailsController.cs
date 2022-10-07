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
    public class BankDetailsController : ApiController
    {
        private BankDataBaseEntities db = new BankDataBaseEntities();

        // GET: api/BankDetails
        public IQueryable<BankDetail> GetBankDetails()
        {
            return db.BankDetails;
        }

        // GET: api/BankDetails/5
        [ResponseType(typeof(BankDetail))]
        public IHttpActionResult GetBankDetail(int id)
        {
            BankDetail bankDetail = db.BankDetails.Find(id);
            if (bankDetail == null)
            {
                return NotFound();
            }

            return Ok(bankDetail);
        }

        // PUT: api/BankDetails/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBankDetail(int id, BankDetail bankDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bankDetail.Id)
            {
                return BadRequest();
            }

            db.Entry(bankDetail).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BankDetailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/BankDetails
        [ResponseType(typeof(BankDetail))]
        public IHttpActionResult PostBankDetail(BankDetail bankDetail)
        {
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

            return CreatedAtRoute("DefaultApi", new { id = bankDetail.Id }, bankDetail);
        }

        // DELETE: api/BankDetails/5
        [ResponseType(typeof(BankDetail))]
        public IHttpActionResult DeleteBankDetail(int id)
        {
            BankDetail bankDetail = db.BankDetails.Find(id);
            if (bankDetail == null)
            {
                return NotFound();
            }

            db.BankDetails.Remove(bankDetail);
            db.SaveChanges();

            return Ok(bankDetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BankDetailExists(int id)
        {
            return db.BankDetails.Count(e => e.Id == id) > 0;
        }
    }
}