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
using WebApiKategoriler.Models;

namespace WebApiKategoriler.Controllers
{
    public class UrunlerController : ApiController
    {
        private E_Ticaret_2023Entities db = new E_Ticaret_2023Entities();

        // GET: api/Urunler
        public List<Urun> GetUrunler()
        {
            List<Urunler> urunler = db.Urunler.ToList();
            List<Urun> urun = new List<Urun>();
            foreach (Urunler item in urunler)
            {
                urun.Add(new Urun {UrunId=item.UrunId,KategoriId=item.KategoriId, UrunAciklamasi=item.UrunAciklamasi, UrunAdi=item.UrunAdi, UrunFiyati=item.UrunFiyati});
            }
            return urun;
        }

        // GET: api/Urunler/5
        [ResponseType(typeof(Urunler))]
        public IHttpActionResult GetUrunler(int id)
        {
            Urunler urunler = db.Urunler.Find(id);
            if (urunler == null)
            {
                return NotFound();
            }

            return Ok(urunler);
        }

        // PUT: api/Urunler/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUrunler(int id, Urunler urunler)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != urunler.UrunId)
            {
                return BadRequest();
            }

            db.Entry(urunler).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UrunlerExists(id))
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

        // POST: api/Urunler
        [ResponseType(typeof(Urunler))]
        public IHttpActionResult PostUrunler(Urunler urunler)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Urunler.Add(urunler);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = urunler.UrunId }, urunler);
        }

        // DELETE: api/Urunler/5
        [ResponseType(typeof(Urunler))]
        public IHttpActionResult DeleteUrunler(int id)
        {
            Urunler urunler = db.Urunler.Find(id);
            if (urunler == null)
            {
                return NotFound();
            }

            db.Urunler.Remove(urunler);
            db.SaveChanges();

            return Ok(urunler);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UrunlerExists(int id)
        {
            return db.Urunler.Count(e => e.UrunId == id) > 0;
        }
    }
}