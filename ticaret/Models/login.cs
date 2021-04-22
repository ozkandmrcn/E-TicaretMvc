using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ticaret.Models
{
    public class login
    {
        public int id { get; set; }
        public string kullanici_adi { get; set; }
        public string sifre { get; set; }

        public string isim { get; set; }
        public string soyisim { get; set; }
        public string tc{ get; set; }
        public string tel { get; set; }
        public string email { get; set; }
        public string dogrulama { get; set; }
        public int yetki{ get; set; }


        public string Command { get; set; }
    }
}