using proyectoCakeAPI7.DAO;
using proyectoCakeAPI7.DAO.DaoImpl;
using proyectoCakeAPI7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace proyectoCakeAPI7.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/sabor")]
    public class SaborController : ApiController
    {
        private readonly ISaborDAO dao = new SaborDaoImpl();

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
        public IHttpActionResult Post([FromBody] Sabor sabor)
        {
            dao.Insert(sabor);
            return Ok(new { mensaje = "Sabor insertado correctamente" });
        }

        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put(int id, [FromBody] Sabor sabor)
        {
            dao.Update(id, sabor);
            return Ok(new { mensaje = "Sabor actualizado correctamente" });
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            dao.Delete(id);
            return Ok(new { mensaje = "Sabor eliminado correctamente" });
        }
    }
}
