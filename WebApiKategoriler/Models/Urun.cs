using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiKategoriler.Models
{
    public class Urun
    {
        public int UrunId { get; set; }
        public string UrunAdi { get; set; }
        public int? KategoriId { get; set; }
        public string UrunAciklamasi { get; set; }
        public int UrunFiyati { get; set; }

    }
}