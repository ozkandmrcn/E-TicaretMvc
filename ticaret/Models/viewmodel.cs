using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ticaret.Models
{
    public class viewmodel
    {
        public IEnumerable<Kategori> Kategori { get; set; }
        public IEnumerable<Marka> Marka { get; set; }

       

    }
}