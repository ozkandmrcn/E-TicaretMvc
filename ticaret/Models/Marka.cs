using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ticaret.Models
{
    public class Marka
    {
        public Marka()
        {
            this.Uruns = new List<urunler>();
        }

        public int Id { get; set; }
        public string Adi { get; set; }
        public string Aciklama { get; set; }
        public string resim { get; set; }

        public string sonuc { get; set; }
        public Nullable<int> ResimID { get; set; }
        public virtual Resim Resim { get; set; }
        public virtual ICollection<urunler> Uruns { get; set; }

        public string Command { get; set; }
    }
}