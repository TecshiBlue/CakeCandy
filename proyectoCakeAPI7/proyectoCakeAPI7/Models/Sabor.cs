using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proyectoCakeAPI7.Models
{
    public class Sabor
    {
        public int idSabor { get; set; }
        public string nombreSabor { get; set; }

        public Sabor() { }

        public Sabor(int idSabor, string nombreSabor)
        {
            this.idSabor = idSabor;
            this.nombreSabor = nombreSabor;
        }
    }
}