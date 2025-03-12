using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyonV1.Models.Siniflar;
using PagedList;
using PagedList.Mvc;

namespace MvcOnlineTicariOtomasyonV1.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        Context c = new Context();
        public ActionResult Index(string p, int sayfa = 1)
        {
            return View(c.Urunlers.Where(x => x.UrunAd.Contains(p) || p == null).ToList().ToPagedList(sayfa, 6));

            // var urunler=c.Urunlers.Where(x=> x.Durum == true).ToList().ToPagedList(sayfa, 4);
            //return View(urunler);
        }

        [HttpGet]
        public ActionResult YeniUrun()  // ürün ekleme
        {
            List<SelectListItem> deger1 = (from x in c.Kategoris.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.KategoriAd,
                                               Value = x.KategoriID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            return View();
        }
        [HttpPost]
        public ActionResult YeniUrun(Urunler p)  // nesne türetme
        {
            c.Urunlers.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Urunsil(int id)
        {
            var deger = c.Urunlers.Find(id);
            deger.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult UrunGetir(int id)
        {
            List<SelectListItem> deger1 = (from x in c.Kategoris.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.KategoriAd,
                                               Value = x.KategoriID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            var urundeger = c.Urunlers.Find(id);
            return View("UrunGetir", urundeger);
        }

        public ActionResult UrunGuncelle(Urunler p)
        {
            var urn = c.Urunlers.Find(p.UrunID);
            urn.AlisFiyat = p.AlisFiyat;
            urn.Durum = p.Durum;
            urn.Kategoriid = p.Kategoriid;
            urn.Marka = p.Marka;
            urn.SatisFiyat = p.SatisFiyat;
            urn.Stok = p.Stok;
            urn.UrunAd = p.UrunAd;
            urn.UrunGorsel = p.UrunGorsel;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunListesi()
        {
            var degerler = c.Urunlers.ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult SatisYap(int id)
        {
            List<SelectListItem> deger3 = (from x in c.Personels.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.PersonelAd + " " + x.PersonelSoyad,
                                               Value = x.PersonelID.ToString()
                                           }).ToList();

            ViewBag.dgr3 = deger3;
            var deger1 = c.Urunlers.Find(id);
            ViewBag.dgr1 = deger1.UrunID;
            ViewBag.dgr2 = deger1.SatisFiyat;
            return View();
        }
        [HttpPost]
        public ActionResult SatisYap(SatisHareket s)
        {
            s.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            c.SatisHarekets.Add(s);
            c.SaveChanges();
            return RedirectToAction("Index","Satis");
        }
    }
}