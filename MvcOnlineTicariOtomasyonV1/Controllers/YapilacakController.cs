using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Schema;
using MvcOnlineTicariOtomasyonV1.Models.Siniflar;
namespace MvcOnlineTicariOtomasyonV1.Controllers
{
    public class YapilacakController : Controller
    {
       Context c = new Context();
        // GET: Yapilacak
        public ActionResult Index()
        {
            var deger1 = c.Caris.Count().ToString();
            ViewBag.dg1 = deger1;
            var deger2 = c.Urunlers.Count().ToString();
            ViewBag.dg2 = deger2;   
            var deger3 = c.Kategoris.Count().ToString();
            ViewBag.dg3 = deger3;
            var deger4 = (from x in c.Caris select x.CariSehir).Distinct().Count().ToString();
            ViewBag.dg4 = deger4;

            var yapilacak = c.Yapilacaks.ToList();
            return View(yapilacak);
        }
    }
}