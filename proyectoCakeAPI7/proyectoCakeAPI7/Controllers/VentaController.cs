using proyectoCakeAPI7.DAO.DaoImpl.TransaccionImpl;
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
        [RoutePrefix("api/venta")]
        public class VentaController : ApiController
        {
            private readonly IVentaDAO dao = new VentaDaoImpl();

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

            [HttpPost]
            [Route("")]
            public IHttpActionResult Post([FromBody] Venta venta)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                dao.Insert(venta);
                return Ok(new { mensaje = "Venta insertada correctamente" });
            }

            [HttpPut]
            [Route("{id:int}")]
            public IHttpActionResult Put(int id, [FromBody] Venta venta)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (dao.GetById(id) == null)
                    return NotFound();

                dao.Update(id, venta);
                return Ok(new { mensaje = "Venta actualizada correctamente" });
            }

            [HttpDelete]
            [Route("{id:int}")]
            public IHttpActionResult Delete(int id)
            {
                if (dao.GetById(id) == null)
                    return NotFound();

                dao.Delete(id);
                return Ok(new { mensaje = "Venta eliminada correctamente" });
            }
        }
}
