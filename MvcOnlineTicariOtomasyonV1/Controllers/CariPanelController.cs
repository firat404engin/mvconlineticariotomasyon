using MvcOnlineTicariOtomasyonV1.Models.Siniflar;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcOnlineTicariOtomasyonV1.Controllers
{
    public class CariPanelController : Controller
    {
        Context c = new Context();
        // GET: CariPanel
        [Authorize]
        public ActionResult Index()
        {
            var mail = (string)Session["CariMail"];
            var degerler = c.Mesajlars.Where(x => x.Alici == mail).ToList();
            ViewBag.m = mail;
            var mailid = c.Caris.Where(x=>x.CariMail == mail).Select(y=>y.CariID).FirstOrDefault();
            ViewBag.mid = mailid;

            var toplamtutar = c.SatisHarekets.Where(x => x.Cariid == mailid).Sum(y => (decimal?)y.ToplamTutar) ?? 0;
            ViewBag.toplamtutar = toplamtutar;
            var toplamsatis = c.SatisHarekets.Where(x => x.Cariid == mailid).Sum(y => (int?)y.Adet) ?? 0;
            ViewBag.toplamsatis = toplamsatis;
 
            var adsoyad=c.Caris.Where(x=>x.CariMail == mail).Select(y=>y.CariAd + " " + y.CariSoyad).FirstOrDefault();
            ViewBag.adso=adsoyad;
            return View(degerler);
        }
        [Authorize]
        public ActionResult Siparislerim()
        {
            var mail = (string)Session["CariMail"];
            var id = c.Caris.Where(x => x.CariMail == mail.ToString()).Select(y
                => y.CariID).FirstOrDefault();
            var degerler = c.SatisHarekets.Where(x => x.Cariid == id).ToList();
            return View(degerler);
        }
        [Authorize]
        public ActionResult GelenMesajlar()
        {
            var mail = (string)Session["CariMail"]; //oturumu açan carinin mailini tutar
            var mesajlar = c.Mesajlars.Where(x => x.Alici == mail).OrderByDescending(x=>x.MesajID).ToList();
            var gidensayi = c.Mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.d2 = gidensayi;
            var gelensayisi = c.Mesajlars.Count(x => x.Alici == mail).ToString();
            ViewBag.d1 = gelensayisi;
            return View(mesajlar);
        }
        [Authorize]
        public ActionResult GidenMesajlar() // mesaj sayilarini ve carinini mailini al
        {
            var mail = (string)Session["CariMail"]; //oturumu açan carinin mailini tutar
            var mesajlar = c.Mesajlars.Where(x => x.Gonderici == mail).OrderByDescending(x => x.MesajID).ToList();
            var gelensayisi = c.Mesajlars.Count(x => x.Alici == mail).ToString();
            ViewBag.d1 = gelensayisi;
            var gidensayi = c.Mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.d2 = gidensayi;
            return View(mesajlar);
        }
        [Authorize]
        public ActionResult MesajDetay(int id)
        {
            var degerler = c.Mesajlars.Where(x => x.MesajID == id).ToList();
            var mail = (string)Session["CariMail"]; //oturumu açan carinin mailini tutar
            var gelensayisi = c.Mesajlars.Count(x => x.Alici == mail).ToString();
            ViewBag.d1 = gelensayisi;
            var gidensayi = c.Mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.d2 = gidensayi;
            return View(degerler);
        }

        [Authorize]
        [HttpGet]
        public ActionResult YeniMesaj()
        {
            var mail = (string)Session["CariMail"]; //oturumu açan carinin mailini tutar
            var gelensayisi = c.Mesajlars.Count(x => x.Alici == mail).ToString();
            ViewBag.d1 = gelensayisi;
            var gidensayi = c.Mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.d2 = gidensayi;
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult YeniMesaj(Mesajlar mesaj)
        {
            var mail = (string)Session["CariMail"]; //oturumu açan carinin mailini tutar
            mesaj.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            mesaj.Gonderici = mail;
            c.Mesajlars.Add(mesaj);
            c.SaveChanges();
            return View();
        }
        [Authorize]
        public ActionResult KargoTakip(string p, int sayfa = 1)
        {
            return View(c.KargoDetays.Where(x => x.TakipKodu.Contains(p)).ToList().ToPagedList(sayfa, 6));
            //var kargolar = c.KargoDetays.ToList();
            //return View(kargolar);
        }
        [Authorize]
        public ActionResult CariKargoTakip(string id)
        {
            var degerler = c.KargoTakips.Where(x => x.TakipKodu == id).ToList();
            return View(degerler);
        }
        [Authorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index","Login");
        }

        public PartialViewResult Partial1()
        {
            var mail = (string)Session["CariMail"]; //oturumu açan carinin mailini tutar
            var id = c.Caris.Where(x=>x.CariMail ==mail).Select(y=>y.CariID).FirstOrDefault();
            var caribul = c.Caris.Find(id);
            return PartialView("Partial1", caribul);
        }

        public PartialViewResult Partial2()
        {
            var veriler = c.Mesajlars.Where(x => x.Gonderici == "admin").ToList();
            return PartialView(veriler);
        }

        public ActionResult CariBilgiGuncelle(Cari cr)
        {
            var cari = c.Caris.Find(cr.CariID);
            cari.CariAd = cr.CariAd;
            cari.CariSoyad = cr.CariSoyad;
            cari.CariSifre = cr.CariSifre;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}