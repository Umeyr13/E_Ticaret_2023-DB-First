using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using E_Ticaret_2023.Models;
using Newtonsoft.Json;

namespace E_Ticaret_2023.Controllers
{
    [Authorize(Roles = "admin")]
    public class KategoriController : Controller
    {
        private E_Ticaret_2023Entities1 db = new E_Ticaret_2023Entities1();
        HttpClient client = new HttpClient();

        public ActionResult Index()
        {
            List<Kategoriler> liste = new List<Kategoriler>();
            client.BaseAddress = new Uri("https://localhost:44319/api/Kategori2_deneme");
            var cevap = client.GetAsync("Kategori2_deneme");//Asenkron metot araştır. diğer kodu beklemez.
            cevap.Wait();
            if(cevap.Result.IsSuccessStatusCode)//Benim respons başarılı oldu mu
            {
                //gelen veri json türünden bir data
               var data = cevap.Result.Content.ReadAsStringAsync();//İçeriği oku
                data.Wait();
                liste = JsonConvert.DeserializeObject<List<Kategoriler>>(data.Result);//json verisini istediğimiz türe dönüştürdük.
            }
            return View(liste);
            //api den veri almayı denemek için bunu kapatıyoruz şimdilik
            //return View(db.Kategoriler.ToList());
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategoriler kategoriler =KategoriBul(id.Value); //db.Kategoriler.Find(id);
            if (kategoriler == null)
            {
                return HttpNotFound();
            }
            return View(kategoriler);
        }

        //detay için api kullanalım
        private Kategoriler KategoriBul(int id)
        {
            Kategoriler kategori = null;//kategori çekemezse null döndürsün diye
                                        //kategori = db.Kategoriler.Find(id); bunun yerine api den çek db den değil diyelim.

            List<Kategoriler> liste = new List<Kategoriler>();
            client.BaseAddress = new Uri("https://localhost:44319/api/Kategori2_deneme");
            var cevap = client.GetAsync("Kategori2_deneme/"+id.ToString());
            cevap.Wait();
            if (cevap.Result.IsSuccessStatusCode)
            {
                var data = cevap.Result.Content.ReadAsStringAsync();
                data.Wait();
                kategori = JsonConvert.DeserializeObject<Kategoriler>(data.Result);
            }

            return kategori;
        }

        // GET: Kategori/Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Kategoriler kategoriler)
        {
            if (ModelState.IsValid)
            {
                //db.Kategoriler.Add(kategoriler);
                //db.SaveChanges();
                //return RedirectToAction("Index"); api den yapalım;

                client.BaseAddress = new Uri("https://localhost:44319/api/Kategori2_deneme");
                var cevap = client.PostAsJsonAsync<Kategoriler>("Kategori2_deneme",kategoriler);
                cevap.Wait();
                if (cevap.Result.IsSuccessStatusCode)
                {

                }return RedirectToAction("Index");


            }

            return View(kategoriler);
        }

        // GET: Kategori/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategoriler kategoriler = KategoriBul(id.Value); //db.Kategoriler.Find(id);
            if (kategoriler == null)
            {
                return HttpNotFound();
            }
            return View(kategoriler);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Kategoriler kategoriler)
        {
            if (ModelState.IsValid)
            {
                kategoriler.KategoriAdi= kategoriler.KategoriAdi.Trim();//bazen önerilen kategori adını seçince taryıcıda kendisi sonuna boşluk ekliyordu onları silmek için.
                client.BaseAddress = new Uri("https://localhost:44319/api/Kategori2_deneme");
                var cevap = client.PutAsJsonAsync<Kategoriler>("Kategori2_deneme",kategoriler);
                cevap.Wait();
                if (cevap.Result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                //api ile denemek için kapattım
                //db.Entry(kategoriler).State = EntityState.Modified;
                //db.SaveChanges();
                //return RedirectToAction("Index");
            }
            return View(kategoriler);
        }

   
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategoriler kategoriler = db.Kategoriler.Find(id);
            if (kategoriler == null)
            {
                return HttpNotFound();
            }
            return View(kategoriler);
        }

    
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            client.BaseAddress = new Uri("https://localhost:44319/api/Kategori2_deneme");
            var cevap = client.DeleteAsync("Kategori2_deneme/"+id.ToString());
            cevap.Wait();
            if (cevap.Result.IsSuccessStatusCode)
            { 
                return RedirectToAction("Index"); 
                
            }

             return RedirectToAction("Index");

            //Kategoriler kategoriler = KategoriBul(id); //db.Kategoriler.Find(id);
            //db.Kategoriler.Remove(kategoriler);
            //db.SaveChanges();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
