using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using ticaret.Models;

namespace ticaret.Models
{
    [Table("Kategori")]

    public class Kategori
    {
        public Kategori()
        {
           
            this.Uruns = new List<urunler>();
        }
      
        public int Id { get; set; }
        public string Adi { get; set; }
        public string Aciklama { get; set; }
        public Nullable<int> ResimID { get; set; }
        public virtual Resim Resim { get; set; }
       
        public virtual ICollection<urunler> Uruns { get; set; }
        public virtual ICollection<Marka> Marka { get; set; }
    }
}