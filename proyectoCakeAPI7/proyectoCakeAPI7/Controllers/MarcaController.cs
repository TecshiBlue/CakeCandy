using proyectoCakeAPI7.DAO.DaoImpl;
using proyectoCakeAPI7.DAO;
using proyectoCakeAPI7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace proyectoCakeAPI7.Controllers
{
    [RoutePrefix("api/marca")]
    public class MarcaController : ApiController
    {
        private readonly IMarcaDAO dao = new MarcaDaoImpl();

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
        public IHttpActionResult Post([FromBody] Marca marca)
        {
            dao.Insert(marca);
            return Ok(new { mensaje = "Marca insertada correctamente" });
        }

        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put(int id, [FromBody] Marca marca)
        {
            dao.Update(id, marca);
            return Ok(new { mensaje = "Marca actualizada correctamente" });
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            dao.Delete(id);
            return Ok(new { mensaje = "Marca eliminada correctamente" });
        }
    }
}
