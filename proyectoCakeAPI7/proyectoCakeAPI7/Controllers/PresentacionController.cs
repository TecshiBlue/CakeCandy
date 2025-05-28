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
    [RoutePrefix("api/presentacion")]
    public class PresentacionController : ApiController
    {
        private readonly IPresentacionDAO dao = new PresentacionDaoImpl();

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
        public IHttpActionResult Post([FromBody] Presentacion presentacion)
        {
            dao.Insert(presentacion);
            return Ok(new { mensaje = "Presentación insertada correctamente" });
        }

        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put(int id, [FromBody] Presentacion presentacion)
        {
            dao.Update(id, presentacion);
            return Ok(new { mensaje = "Presentación actualizada correctamente" });
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            dao.Delete(id);
            return Ok(new { mensaje = "Presentación eliminada correctamente" });
        }
    }
}
