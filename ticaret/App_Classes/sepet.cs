using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ticaret.Models;

namespace ticaret.App_Classes
{
    public class sepet
    {
        public static sepet AktifSepet
        {
            get
            {
                HttpContext ctx = HttpContext.Current;
                if (ctx.Session["AktifSepet"] == null)
                    ctx.Session["AktifSepet"] = new sepet();

                return (sepet)ctx.Session["AktifSepet"];

            }

        }

        private List<SepetItem> urunler = new List<SepetItem>();

        public List<SepetItem> Urunler
        {
            get { return urunler; }
            set { urunler = value; }
        }

        public void SepeteEkle(SepetItem si)
        {
            if (HttpContext.Current.Session["AktifSepet"] != null)
            {
                sepet s = (sepet)HttpContext.Current.Session["AktifSepet"];
                if (s.Urunler.Any(x => x.Urun.Id == si.Urun.Id))
                    s.Urunler.FirstOrDefault(x => x.Urun.Id == si.Urun.Id).Adet++;
                else
                {
                    s.Urunler.Add(si);
                }
            }
            else
            {
                sepet s = new sepet();
                s.Urunler.Add(si);

                HttpContext.Current.Session["AktifSepet"] = s;
            }

        }
        public decimal ToplamTutar
        {
            get
            {
                return Urunler.Sum(x => x.Tutar);
            }
        }
    }

    public class SepetItem
    {
        public urunler Urun { get; set; }
        public int Adet { get; set; }
        public double Indirim { get; set; }

        public decimal Tutar
        {
            get
            {
                return Urun.Fiyat* Adet * (1 - (decimal)Indirim);
            }
        }



    }
}
