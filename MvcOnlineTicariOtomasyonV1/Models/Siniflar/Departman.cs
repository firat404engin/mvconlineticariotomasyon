using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyonV1.Models.Siniflar
{
	public class Departman
	{
		[Key]
		public int DepartmanId { get; set; }
		[Column(TypeName = "Varchar")]
		[StringLength(30)]
		public string	DepartmanAd { get; set; }
		public bool durum {  get; set; }
        public ICollection<Personel> Personels { get; set; }
    }
}