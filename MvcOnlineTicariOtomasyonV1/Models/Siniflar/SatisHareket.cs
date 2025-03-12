using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyonV1.Models.Siniflar
{
	public class SatisHareket
	{
        [Key]
        public int SatisID { get; set; }
        // ürün
        // cari
        // personel
        public DateTime Tarih { get; set; }
        public int Adet { get; set; }
        public decimal Fiyat { get; set; }
        public decimal ToplamTutar { get; set; }

        public int Urunid {  get; set; }
        public int Cariid { get; set; }
        public int PersonelID {  get; set; }    
        public virtual Urunler  Uruns { get; set; }
        public virtual Cari Cariler { get; set; }
        public virtual Personel Personel { get; set; }
    }
}