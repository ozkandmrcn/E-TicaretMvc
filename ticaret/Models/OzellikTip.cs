using System.Collections.Generic;

namespace ticaret.Models
{
    internal class OzellikTip
    {
        public OzellikTip()
        {
            this.OzellikDegers = new List<OzellikDeger>();
            this.UrunOzelliks = new List<OzellikTip>();
        }

        public int Id { get; set; }
        public string Adi { get; set; }
        public string Aciklama { get; set; }
        public int KategoriID { get; set; }
        public virtual Kategori Kategori { get; set; }
        public virtual ICollection<OzellikDeger> OzellikDegers { get; set; }
        public virtual ICollection<OzellikTip> UrunOzelliks { get; set; }
    }
}