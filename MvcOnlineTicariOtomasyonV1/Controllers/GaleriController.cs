using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyonV1.Models.Siniflar;
namespace MvcOnlineTicariOtomasyonV1.Controllers
{
    public class GaleriController : Controller
    {
        // GET: Galeri
        Context c = new Context();
        public ActionResult Index()
        {
            var degerler = c.Urunlers.ToList();
            return View(degerler);
        }
    }
}