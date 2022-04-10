using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nyilvantartas.Models
{
    public class AruViewModel
    {
        public List<aruk> nyilvantartas { get; set; }
        public string keresMegnevezes { get; set; }
        public string keresTipus { get; set; }

        public SelectList Tipus { get; set; }

    }
}
