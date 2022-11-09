using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebService.Data;
using WebService.Models;

namespace WebService.Controllers
{
    public class UsuarioController : ApiController
    {

        // GET api/<controller>
        public List<Usuario> Get()
        {
            return UsuarioData.listar();
        }

        // GET api/<controller>/5
        public Usuario Get(int idUsuario)
        {
            return UsuarioData.obtener(idUsuario);
        }

        // POST api/<controller>
        public bool Post([FromBody] Usuario oUsuario)
        {
            return UsuarioData.registrar(oUsuario);
        }

        // PUT api/<controller>/5
        public bool Put([FromBody] Usuario oUsuario)
        {
            return UsuarioData.modificar(oUsuario);
        }

        // DELETE api/<controller>/5
        public bool Delete(int idUsuario)
        {
            return UsuarioData.eliminar(idUsuario);
        }
    }
}