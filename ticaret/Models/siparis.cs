using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ticaret.Models
{
    public class siparis
    {
        public int id { get; set; }
        public int kullanici_id { get; set; }
        public int urun_id { get; set; }

        public string adsoyad { get; set; }
        public string adres { get; set; }
        public string toplamfiyat { get; set; }
        public string odemesekli { get; set; }
        public string telefon { get; set; }

        public string kod { get; set; }



        public int kargo_id { get; set; }
    }
}