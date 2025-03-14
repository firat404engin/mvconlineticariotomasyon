﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyonV1.Models.Siniflar
{
	public class FaturaKalem
	{
		[Key]
		public int FaturaKalemID { get; set; }
		[Column(TypeName = "Varchar")]
		[StringLength(100)]
		public string Acıklama { get; set; }
		public int Miktar { get; set; }
		public decimal BirimFiyat { get; set; }
		public decimal Tutar { get; set; }
		public int FaturaID { get; set; }
        public virtual Faturalar Faturalar { get; set; }

    }
}