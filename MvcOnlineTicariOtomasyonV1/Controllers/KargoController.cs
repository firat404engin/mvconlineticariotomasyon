using MvcOnlineTicariOtomasyonV1.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace MvcOnlineTicariOtomasyonV1.Controllers
{
    public class KargoController : Controller
    {
        // GET: Kargo
        Context c = new Context();
        public ActionResult Index(string p, int sayfa = 1)
        {
            return View(c.KargoDetays.Where(x => x.TakipKodu.Contains(p) || p == null).ToList().ToPagedList(sayfa, 6));
            //var kargolar = c.KargoDetays.ToList();
            //return View(kargolar);
        }
        [HttpGet]
        public ActionResult YeniKargo()
        {
            string kargoKod = OlusturKargoKodu();
            ViewBag.KargoKod = kargoKod;
            return View();
        }

        [HttpPost]
        public ActionResult YeniKargo(KargoDetay d)
        {
            c.KargoDetays.Add(d);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KargoTakip(string id)
        {
            var degerler = c.KargoTakips.Where(x => x.TakipKodu == id).ToList();           
            return View(degerler);
        }


        private string OlusturKargoKodu()
        {
            // GUID oluştur ve yalnızca harf ve rakamları al
            string guidString = Guid.NewGuid().ToString("N").ToUpper();

            // İlk 4 karakter rakam, son 6 karakter harf olacak şekilde düzenle
            string numbers = new string(guidString.Where(char.IsDigit).ToArray()).PadRight(4, '0').Substring(0, 4);
            string letters = new string(guidString.Where(char.IsLetter).ToArray()).PadRight(6, 'A').Substring(0, 6);

            // Kargo kodunu oluştur
            return numbers + letters;
        }

    }
}