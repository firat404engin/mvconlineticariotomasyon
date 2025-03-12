using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyonV1.Models.Siniflar
{
	public class Cari
	{
        [Key]
        public int CariID { get; set; }
		[Column(TypeName = "Varchar")]
		[StringLength(30,ErrorMessage ="En fazla 30 karakter yazabilirsin ")]
		public string CariAd { get; set; }
		[Column(TypeName = "Varchar")]
		[StringLength(30)]
		[Required(ErrorMessage ="bu alanı boş geçemezsiniz")]
		public string CariSoyad { get; set; }
		[Column(TypeName = "Varchar")]
		[StringLength(15)]
		public string CariSehir { get; set; }

		[Column(TypeName = "Varchar")]
		[StringLength(150)]
		public string CariMail { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(20)]
        public string CariSifre { get; set; }

        public bool durum { get; set; } // silmek için false yapilir gerçek silme yapilmaz
		public ICollection<SatisHareket> SatisHarekets { get; set; }
	}
}