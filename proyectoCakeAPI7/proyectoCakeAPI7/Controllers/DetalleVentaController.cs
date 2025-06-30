/* using proyectoCakeAPI7.DAO.DaoImpl.TransaccionImpl;
using proyectoCakeAPI7.DAO.Transaccion;
using proyectoCakeAPI7.Models.Transaccion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace proyectoCakeAPI7.Controllers
{
    [RoutePrefix("api/detalleventa")]
    public class DetalleVentaController : ApiController
    {
        private readonly IDetalleVentaDAO dao = new DetalleVentaDaoImpl();

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll() => Ok(dao.GetAll());

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            var result = dao.GetById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet]
        [Route("venta/{ventaId:int}")]
        public IHttpActionResult GetByVentaId(int ventaId)
        {
            var result = dao.GetByVentaId(ventaId);
            if (result == null || result.Count == 0) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody] DetalleVenta detalle)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            dao.Insert(detalle);
            return Ok(new { mensaje = "Detalle de venta insertado correctamente" });
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            if (dao.GetById(id) == null)
                return NotFound();

            dao.Delete(id);
            return Ok(new { mensaje = "Detalle de venta eliminado correctamente" });
        }
    }
} */
