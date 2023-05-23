using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using WebApiKategoriler.Models;

namespace WebApiKategoriler.Controllers
{
    public class KategorilerController : ApiController
    {
        E_Ticaret_2023Entities db = new E_Ticaret_2023Entities();
        public List<Kategori> Get()
        {
            List<Kategoriler> liste = db.Kategoriler.ToList();// db den listeyi çekip return edicek fakat liste de bağlı yerler olduğu için misal ürünler tablosu ile bağlı. Kodda neyi getireceğini tam olarak bilemiyor ve herşeyi getirmeye çalışıyor.

            List<Kategori> kategoriler = new List<Kategori>();

            //bizde gelen veriyi ayrıştırıyoruz..

            foreach (Kategoriler item in liste)
            {
                kategoriler.Add(new Kategori() { Kategoriadi = item.KategoriAdi, KategoriId = item.KategoriId });
            }

            // veya LinQ ile veriyi ayrıştırmayı deneyelim

            kategoriler = (from x in db.Kategoriler select new Kategori { KategoriId = x.KategoriId, Kategoriadi = x.KategoriAdi }).ToList();

            return kategoriler;
        }

        public Kategori Get (int id)
        {
            Kategoriler kategoriler = db.Kategoriler.Find(id);
            Kategori kategori = new Kategori() { Kategoriadi=kategoriler.KategoriAdi,KategoriId=kategoriler.KategoriId};
            return kategori;
        }

    }
}
