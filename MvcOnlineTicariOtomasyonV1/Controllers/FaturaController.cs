using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyonV1.Models.Siniflar;

namespace MvcOnlineTicariOtomasyonV1.Controllers
{
    public class FaturaController : Controller
    {
        // GET: Fatura
        Context c = new Context();
        public ActionResult Index()
        {
            var liste = c.Faturalars.ToList();
            return View(liste);
        }

        [HttpGet]
        public ActionResult FaturaEkle() 
        {
            return View(); 
        }
		[HttpPost]
		public ActionResult FaturaEkle(Faturalar f)
		{
            c.Faturalars.Add(f);
            c.SaveChanges();
            return RedirectToAction("Index");
		}
        public ActionResult FaturaGetir(int id) 
        {
            var fatura = c.Faturalars.Find(id);
            return View("FaturaGetir",fatura);
        }
        public ActionResult FaturaGuncelle(Faturalar f )
        {
            var fatura = c.Faturalars.Find(f.FaturaID);
            fatura.FaturaSeriNo = f.FaturaSeriNo;
            fatura.FaturaSıraNo = f.FaturaSıraNo;
            fatura.Saat = f.Saat;
            fatura.FaturaTarih = f.FaturaTarih;
            fatura.VergiDairesi = f.VergiDairesi;
            fatura.TeslimAlan = f.TeslimAlan;
            fatura.TeslimEden  = f.TeslimEden;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult FaturaDetay(int id)
        {
			var degerler = c.FaturaKalems.Where(x => x.FaturaID == id).ToList();
			return View(degerler);
		}
        [HttpGet]
        public ActionResult YeniKalem()
        {
            return View();
        }
		public ActionResult YeniKalem(FaturaKalem p)
        { 
            c.FaturaKalems.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Dinamik()
        {
            Class4 cs = new Class4();
            cs.deger1 =c.Faturalars.ToList();
            cs.deger2 = c.FaturaKalems.ToList();
            return View(cs);
        }

        public ActionResult FaturaKaydet(string FaturaSeriNo,string FaturaSıraNo,DateTime FaturaTarih, string VergiDairesi,string Saat,string TeslimEden, string TeslimAlan,string Toplam, FaturaKalem[] kalemler)
        {
            Faturalar f = new Faturalar();
            f.FaturaSeriNo = FaturaSeriNo;
            f.FaturaSıraNo = FaturaSıraNo;
            f.FaturaTarih = FaturaTarih;
            f.VergiDairesi = VergiDairesi;
            f.Saat = Saat;
            f.TeslimAlan = TeslimAlan;
            f.TeslimEden = TeslimEden;
            f.Toplam = decimal.Parse(Toplam);
            c.Faturalars.Add(f);
            c.SaveChanges();
            return Json("İşlem Başarılı",JsonRequestBehavior.AllowGet);
        }
	}
}