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
    public class Kategori2_denemeController : ApiController
    {
        private E_Ticaret_2023Entities db = new E_Ticaret_2023Entities();

        // GET: api/Kategori2_deneme
        public List<Kategori> GetKategoriler()
        {
            List<Kategoriler> liste = db.Kategoriler.ToList();//Kategoriler Controllerdaki aynı sebeten bunarı değiştirdik
            List<Kategori> kategori_ler = new List<Kategori>();
            foreach (Kategoriler item in liste)
            {
                kategori_ler.Add(new Kategori() { Kategoriadi = item.KategoriAdi, KategoriId = item.KategoriId });
            }
            return kategori_ler;
        }

        // GET: api/Kategori2_deneme/5
        [ResponseType(typeof(Kategoriler))]
        public IHttpActionResult GetKategoriler(int id)
        {
            Kategoriler kategoriler = db.Kategoriler.Find(id);
            if (kategoriler == null)
            {
               
                return NotFound();
            }
                //düzenleme
            Kategori kategori = new Kategori() { Kategoriadi = kategoriler.KategoriAdi, KategoriId = kategoriler.KategoriId };
            return Ok(kategori);//Ok yani işlem tamam okey demek
        }

        // PUT: api/Kategori2_deneme/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutKategoriler(Kategoriler kategoriler)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //if (id != kategoriler.KategoriId)
            //{
            //    return BadRequest();
            //}

            db.Entry(kategoriler).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KategorilerExists(kategoriler.KategoriId))
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

        // POST: api/Kategori2_deneme
        [ResponseType(typeof(Kategoriler))]
        public IHttpActionResult PostKategoriler(Kategoriler kategoriler)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Kategoriler.Add(kategoriler);
            db.SaveChanges();

            //return CreatedAtRoute("DefaultApi", new { id = kategoriler.KategoriId }, kategoriler);
            return Ok();//bu da olur. 200 döndürür
        }

        // DELETE: api/Kategori2_deneme/5
        [ResponseType(typeof(Kategoriler))]
        public IHttpActionResult DeleteKategoriler(int id)
        {
            Kategoriler kategoriler = db.Kategoriler.Find(id);
            if (kategoriler == null)
            {
                return NotFound();
            }

            db.Kategoriler.Remove(kategoriler);
            db.SaveChanges();

            return Ok(kategoriler);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KategorilerExists(int id)
        {
            return db.Kategoriler.Count(e => e.KategoriId == id) > 0;
        }
    }
}