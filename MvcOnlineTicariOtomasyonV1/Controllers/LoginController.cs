using MvcOnlineTicariOtomasyonV1.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcOnlineTicariOtomasyonV1.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        Context c = new Context();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult Partial1()
        {
            return PartialView();
        }

        [HttpPost]
        public PartialViewResult Partial1(Cari p)
        {
            if (ModelState.IsValid)
            {
                // CariMail adresine göre aynı kaydın veritabanında olup olmadığını kontrol et
                var existingRecord = c.Caris.FirstOrDefault(c => c.CariMail == p.CariMail);

                if (existingRecord != null)
                {
                    // Eğer aynı mail adresi varsa, kullanıcıya bir hata mesajı ekleyin
                    ModelState.AddModelError("CariMail", "Bu mail adresi zaten kayıtlı.");
                    return PartialView(); // Hata durumunda partial view'i yeniden göster
                }

                // Eğer kayıt yoksa, yeni kaydı ekleyin
                p.durum = true;
                c.Caris.Add(p);
                c.SaveChanges();

                // Başarılı işlem sonrası kullanıcıyı yönlendirme
                return PartialView("Index"); // Yönlendirme veya başka işlem yapabilirsiniz
            }

            // Eğer model geçersizse hata mesajlarını göster
            return PartialView();
        }

        [HttpGet]
        public ActionResult CariLogin1()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CariLogin1(Cari p)
        {
            var bilgiler = c.Caris.FirstOrDefault(x=>x.CariMail==p.CariMail && x.CariSifre == p.CariSifre);
            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.CariMail, false);
                Session["CariMail"] = bilgiler.CariMail.ToString();
                return RedirectToAction("Index", "CariPanel");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpGet]
        public ActionResult AdminLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminLogin(Admin p)
        {
            var bilgiler = c.Admins.FirstOrDefault(x=>x.KullaniciAd==p.KullaniciAd && x.Sifre==p.Sifre);
            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.KullaniciAd, false);
                Session["KullaniciAd"]= bilgiler.KullaniciAd.ToString();
                return RedirectToAction("Index", "Kategori");
            }
            else
            {
                return RedirectToAction("Index","Login");
            }
           
        }
    }
}