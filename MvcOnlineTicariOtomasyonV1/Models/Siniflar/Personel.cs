﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyonV1.Models.Siniflar
{
	public class Personel
	{
		[Key]
        public int PersonelID { get; set; }

		[Display(Name ="Personel Adı")]
		[Column(TypeName = "Varchar")]
		[StringLength(30)]
		public string PersonelAd {  get; set; }

        [Display(Name = "Personel Soyadı")]
        [Column(TypeName = "Varchar")]
		[StringLength(30)]
		public string PersonelSoyad {  get; set; }

        [Display(Name = "Personel Profili")]
        [Column(TypeName = "Varchar")]
		[StringLength(250)]
		public string PersonelGorsel {  get; set; }


		public ICollection<SatisHareket> SatisHarekets { get; set; }
		public int Departmanid { get; set; }	
		public virtual Departman Departman { get; set; }
    }
}