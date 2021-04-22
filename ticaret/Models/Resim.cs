using System;
using System.Collections.Generic;

namespace ticaret.Models
{
    public class Resim
    {


        public Resim()
        {
          
            this.Markas = new List<Marka>();
        }

        public int Id { get; set; }
        public string BuyukYol { get; set; }
        public string OrtaYol { get; set; }
        public string KucukYol { get; set; }
        public Nullable<bool> Varsayilan { get; set; }
        public Nullable<byte> SiraNo { get; set; }
        public Nullable<int> UrunID { get; set; }
        
        public virtual ICollection<Marka> Markas { get; set; }
        public virtual urunler Urun { get; set; }
    }
}