using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using E_Ticaret_2023.Models;
using Microsoft.AspNet.Identity;

namespace E_Ticaret_2023.Controllers
{

    [Authorize] //Eğer sepete giriş yapmadan ulaşsın ben sepeti cookie de tutayım. Kullanıcı login olduğunda da sepet ıd ile kullanıcı id yi eşliyeyim desek o zaman Authorize ı kaldırmak gerekir. Şimdilik üye girişi şart olarak kalsın..
    public class SepetController : Controller
    {
        E_Ticaret_2023Entities1 db = new E_Ticaret_2023Entities1();
       
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            List<Sepet> sepet = db.Sepet.Where(x =>x.KullaniciId== userId).ToList(); // sadece o anki kullanıcının sepetini getir.
            return View(sepet);
        }
     
        public ActionResult SepeteEkle(int UrunId, int adet) //isimler aynı olmalı urun view deki ile
        {

            string userId = User.Identity.GetUserId();
            Urunler urun = db.Urunler.Find(UrunId);
            Sepet sepettekiUrun = db.Sepet.FirstOrDefault(x => x.UrunId == UrunId && x.KullaniciId==userId);
            if (sepettekiUrun == null)
            {
                Sepet sepet = new Sepet() { KullaniciId=userId, UrunId=UrunId,Adet=adet,ToplamTutar=adet*urun.UrunFiyati};
                db.Sepet.Add(sepet);
            }
            else
            {
                sepettekiUrun.Adet+=adet;
                sepettekiUrun.ToplamTutar = sepettekiUrun.Adet*urun.UrunFiyati;
            }
                       
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SepetGuncelle(int SepetId,int adet)//isimlere dikkat!
        {
            Sepet sepet = db.Sepet.Find(SepetId);
            if (sepet ==null)
            {
                return HttpNotFound();
            }
            Urunler urun = db.Urunler.Find(sepet.UrunId);
            sepet.Adet = adet;
            sepet.ToplamTutar = sepet.Adet* urun.UrunFiyati;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SepetSil(int id)
        {
            Sepet sepet = db.Sepet.Find(id);
            if (sepet!=null)
            {
                db.Sepet.Remove(sepet);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}