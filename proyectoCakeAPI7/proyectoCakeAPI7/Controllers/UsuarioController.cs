using proyectoCakeAPI7.Auth;
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
    [RoutePrefix("api/usuario")]
    public class UsuarioController : ApiController
    {
        private readonly IUsuarioDAO usuarioDAO = new UsuarioDaoImpl();


        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login([FromBody] UsuarioLoginRequest req)
        {
            try
            {
                if (req == null)
                    return BadRequest("Datos de solicitud inválidos");

                if (string.IsNullOrWhiteSpace(req.nombre) || string.IsNullOrWhiteSpace(req.password))
                    return BadRequest("Nombre de usuario y contraseña son requeridos");

                Usuario esValido = usuarioDAO.ValidarCredenciales(req.nombre, req.password);

                if (esValido == null)
                    return Unauthorized();

                string token = JwtManager.GenerateToken(esValido.nombre, esValido.usuarioID, esValido.idRol);


                return Ok(new
                {
                    mensaje = "Login correcto",
                    token,
                    usuario = new { esValido.usuarioID, esValido.nombre, esValido.idRol }
                });
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception("Ocurrió un error al procesar tu solicitud de login", ex));
            }
        }

        [HttpPost]
        [Route("register")]
        public IHttpActionResult Register([FromBody] UsuarioLoginRequest req)
        {
            try
            {
                if (req == null)
                    return BadRequest("Datos de solicitud inválidos");

                if (string.IsNullOrWhiteSpace(req.nombre) || string.IsNullOrWhiteSpace(req.password) || req.idRol == 0)  
                    return BadRequest("Nombre de usuario, contraseña y rol son requeridos");

                var nuevoUsuario = new Usuario { nombre = req.nombre, passwordHash = req.password, idRol= req.idRol}; 
                usuarioDAO.RegistrarUsuario(nuevoUsuario);

                return Ok(new { mensaje = "Usuario registrado correctamente" });
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception("Ocurrió un error al registrar el usuario", ex));
            }
        }

        [Authorize]
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            try
            {
                var usuarios = usuarioDAO.GetAll();
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception("Error al obtener la lista de usuarios", ex));
            }
        }

        [Authorize]
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            try
            {
                var usuario = usuarioDAO.GetById(id);
                if (usuario == null)
                    return NotFound();

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"Error al obtener el usuario con ID {id}", ex));
            }
        }

        [Authorize]
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Update(int id, [FromBody] Usuario usuario)
        {
            try
            {
                if (usuario == null)
                    return BadRequest("Datos de usuario inválidos");

                if (string.IsNullOrWhiteSpace(usuario.nombre) || string.IsNullOrWhiteSpace(usuario.passwordHash) || usuario.idRol == 0)
                    return BadRequest("Nombre y contraseña son requeridos");

                usuarioDAO.Update(id, usuario);
                return Ok(new { mensaje = "Usuario actualizado correctamente" });
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"Error al actualizar el usuario con ID {id}", ex));
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var usuarioExistente = usuarioDAO.GetById(id);
                if (usuarioExistente == null)
                    return NotFound();

                usuarioDAO.Delete(id);
                return Ok(new { mensaje = "Usuario eliminado correctamente" });
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"Error al eliminar el usuario con ID {id}", ex));
            }
        }

        [Authorize]
        [HttpGet]
        [Route("actual")]
        public IHttpActionResult UsuarioActual()
        {
            var identity = (System.Security.Claims.ClaimsIdentity)User.Identity;

            var nombre = identity.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;
            var usuarioID = identity.FindFirst("usuarioID")?.Value;
            var idRol = identity.FindFirst("idRol")?.Value;

            return Ok(new
            {
                usuarioID,
                nombre,
                idRol
            });
        }


    }
}
