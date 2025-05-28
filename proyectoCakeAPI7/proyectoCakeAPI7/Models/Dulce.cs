using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proyectoCakeAPI7.Models
{
    public class Dulce
    {
        public int idDulce { get; set; }
        public string nombreDulce { get; set; }
        public decimal precioDulce { get; set; }
        public int stockDulce { get; set; }
        public int idSabor { get; set; }
        public int idCategoria { get; set; }
        public int idMarca { get; set; }
        public int idPresentacion { get; set; }

        public Dulce() { }

        public Dulce(int idDulce, string nombreDulce, decimal precioDulce, int stockDulce,
                     int idSabor, int idCategoria, int idMarca, int idPresentacion)
        {
            this.idDulce = idDulce;
            this.nombreDulce = nombreDulce;
            this.precioDulce = precioDulce;
            this.stockDulce = stockDulce;
            this.idSabor = idSabor;
            this.idCategoria = idCategoria;
            this.idMarca = idMarca;
            this.idPresentacion = idPresentacion;
        }
    }
}