﻿using E_Ticaret_2023.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Ticaret_2023.Controllers
{
    public class HomeController : Controller
    {
        E_Ticaret_2023Entities1 db = new E_Ticaret_2023Entities1 ();
        public ActionResult Index()
        {
            ViewBag.KategoriListesi = db.Kategoriler.ToList ();
            ViewBag.UrunListesi = db.Urunler.ToList ();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

       
        public ActionResult Urun(int id)
        {
            ViewBag.KategoriListesi = db.Kategoriler.ToList();
            
            return View(db.Urunler.Find(id));
        }

        public ActionResult Kategori(int id)
        {
            ViewBag.KategoriListesi = db.Kategoriler.ToList();
            ViewBag.Kategori = db.Kategoriler.Find(id).KategoriAdi;
          return View(db.Urunler.Where(x =>x.KategoriId == id).ToList());

        }
    }
}