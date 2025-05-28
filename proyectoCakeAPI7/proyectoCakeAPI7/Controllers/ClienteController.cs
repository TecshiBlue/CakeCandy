using proyectoCakeAPI7.DAO.DaoImpl.TransaccionImpl;
using proyectoCakeAPI7.DAO;
using proyectoCakeAPI7.Models.Transaccion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace proyectoCakeAPI7.Controllers
{
    [RoutePrefix("api/cliente")]
    public class ClienteController : ApiController
    {
        private readonly IClienteDAO dao = new ClienteDaoImpl();

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
        public IHttpActionResult Post([FromBody] Cliente cliente)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            dao.Insert(cliente);
            return Ok(new { mensaje = "Cliente insertado correctamente" });
        }

        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put(int id, [FromBody] Cliente cliente)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (dao.GetById(id) == null)
                return NotFound();

            dao.Update(id, cliente);
            return Ok(new { mensaje = "Cliente actualizado correctamente" });
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            if (dao.GetById(id) == null)
                return NotFound();

            dao.Delete(id);
            return Ok(new { mensaje = "Cliente eliminado correctamente" });
        }
    }
}
