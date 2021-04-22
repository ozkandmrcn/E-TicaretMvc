using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ticaret.Models
{
    public class urunler
    {


        public int Id { get; set; }
        public string Adi { get; set; }
        public decimal Fiyat { get; set; }
        public string beden { get; set; }
        public string  ozellık { get; set; }
        public string resim { get; set; }
        public string adet { get; set; }

        public string EklenmeTarihi { get; set; }


        public Nullable<int> KategoriID { get; set; }
        public Nullable<int> MarkaID { get; set; }
        




    }
}