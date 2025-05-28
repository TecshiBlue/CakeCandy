using proyectoCakeAPI7.DAO;
using proyectoCakeAPI7.DAO.DaoImpl;
using proyectoCakeAPI7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace proyectoCakeAPI7.Controllers
{
    [RoutePrefix("api/dulce")]
    public class DulceController : ApiController
    {
        private readonly IDulceDAO dao = new DulceDaoImpl();

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            return Ok(dao.GetAll());
        }

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
        public IHttpActionResult Post([FromBody] Dulce dulce)
        {
            dao.Insert(dulce);
            return Ok(new { mensaje = "Dulce insertado correctamente." });
        }

        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put(int id, [FromBody] Dulce dulce)
        {
            var exists = dao.GetById(id);
            if (exists == null) return NotFound();
            dao.Update(id, dulce);
            return Ok(new { mensaje = "Dulce actualizado correctamente." });
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            var exists = dao.GetById(id);
            if (exists == null) return NotFound();
            dao.Delete(id);
            return Ok(new { mensaje = "Dulce eliminado correctamente." });
        }
    }
}
