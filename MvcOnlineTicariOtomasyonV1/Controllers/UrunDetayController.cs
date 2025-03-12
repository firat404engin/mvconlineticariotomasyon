using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyonV1.Models.Siniflar;

namespace MvcOnlineTicariOtomasyonV1.Controllers
{
    public class UrunDetayController : Controller
    {
        // GET: UrunDetay
        Context c = new Context();
        public ActionResult Index()
        {
            Class1 cs = new Class1();
            cs.Deger1 =c.Urunlers.Where(x=>x.UrunID==1).ToList();
            cs.Deger2 =c.Detays.Where(y=>y.DetayID==1).ToList();
          //  var degerler = c.Urunlers.Where(x => x.UrunID==1).ToList();
            return View(cs);
        }
    }
}