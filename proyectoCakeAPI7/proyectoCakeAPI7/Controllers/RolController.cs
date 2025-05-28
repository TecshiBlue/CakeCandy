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
    [RoutePrefix("api/rol")]
    public class RolController : ApiController
    {
        private readonly IRolDAO dao = new RolDaoImpl();

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
        public IHttpActionResult Post([FromBody] Rol rol)
        {
            dao.Insert(rol);
            return Ok(new { mensaje = "Rol insertado correctamente" });
        }

        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put(int id, [FromBody] Rol rol)
        {
            dao.Update(id, rol);
            return Ok(new { mensaje = "Rol actualizado correctamente" });
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            dao.Delete(id);
            return Ok(new { mensaje = "Rol eliminado correctamente" });
        }
    }
}
