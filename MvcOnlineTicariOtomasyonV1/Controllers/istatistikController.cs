using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyonV1.Models.Siniflar;
namespace MvcOnlineTicariOtomasyonV1.Controllers
{
    public class istatistikController : Controller
    {
        // GET: istatistik
        Context c = new Context();
        public ActionResult Index()
        {
            var deger1 = c.Caris.Count().ToString();
			ViewBag.d1 = deger1;
			var deger2 = c.Urunlers.Count().ToString();
            ViewBag.d2=deger2;
			var deger3 = c.Personels.Count().ToString();
			ViewBag.d3 = deger1;
			var deger4 = c.Kategoris.Count().ToString();
			ViewBag.d4 = deger2;
			var deger5= c.Urunlers.Sum(x=> x.Stok).ToString();
			ViewBag.d5 = deger5;
			var deger6 = (from x in c.Urunlers select x.Marka).Distinct().Count().ToString();
			ViewBag.d6 = deger6;
			var deger7 = c.Urunlers.Count(x=>x.Stok <=20 ).ToString();
			ViewBag.d7 = deger7;
			var deger8 = (from x in c.Urunlers orderby x.SatisFiyat descending select x.UrunAd).FirstOrDefault();
			ViewBag.d8 = deger8;
			var deger9 = (from x in c.Urunlers orderby x.SatisFiyat ascending select x.UrunAd).FirstOrDefault();
			ViewBag.d9 = deger9;
			var deger10 = c.Urunlers.GroupBy(x=> x.Marka).OrderByDescending(z => z.Count()).Select(y=>y.Key).FirstOrDefault();
			ViewBag.d10 = deger10;
			var deger11 = c.Urunlers.Count(x => x.UrunAd=="Buzdolabı"). ToString();
			ViewBag.d11 = deger11;
			var deger12 = c.Urunlers.Count(x => x.UrunAd == "Laptop").ToString();
			ViewBag.d12 = deger12;

			var deger13= c.SatisHarekets.Sum(x=> x.ToplamTutar).ToString();
			ViewBag.d13 = deger13;

			DateTime bugun = DateTime.Today;
			var deger14 = c.SatisHarekets.Count(x => x.Tarih==bugun).ToString();
			ViewBag.d14 = deger14;

			var deger15 = c.SatisHarekets.Where(x => x.Tarih == bugun).Sum(y => (decimal?)y.ToplamTutar).ToString();
			ViewBag.d15 = deger15;

			var deger16 = c.SatisHarekets.GroupBy(x => x.Urunid).OrderByDescending(z => z.Count()).Select(y => y.Key).FirstOrDefault();
			var deger17 = c.Urunlers.Where(x => x.UrunID == deger16).Select(k => k.UrunAd).FirstOrDefault(); ;
			ViewBag.d16 = deger17;

			return View();
        }

		public ActionResult KolayTablolar()
		{
			var sorgu = from x in c.Caris
						group x by x.CariSehir into g
						select new SinifGrup
						{
							Sehir = g.Key,
							Sayi = g.Count()
						};
			return View(sorgu.ToList());
		}
		public PartialViewResult Partial1()
		{
			var sorgu2 = from x in c.Personels
						 group x by x.Departman.DepartmanAd into g
						 select new SinifGrup2
						 {
							 Departman = g.Key,
							 Sayi = g.Count()

						 };
			return PartialView(sorgu2);
		}
		public PartialViewResult Partial2()
		{
			var sorgu = c.Caris.ToList();
			return PartialView(sorgu);
		}

		public PartialViewResult Partial3()
		{
			var sorgu = c.Urunlers.ToList();
			return PartialView(sorgu);
		}

		public PartialViewResult Partial4()
		{
            var sorgu2 = from x in c.Urunlers
                         group x by x.Marka into g
                         select new SinifGrup3
                         {
                             marka = g.Key,
                             sayi = g.Count()

                         };
            return PartialView(sorgu2);
        }

    }
}