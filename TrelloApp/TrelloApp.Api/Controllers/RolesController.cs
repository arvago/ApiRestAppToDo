using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using TrelloApp.Api.Request;
using TrelloApp.DomainModel.Entities;
using TrelloApp.OutputAdapter.Repositories;

namespace TrelloApp.Api.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/roles")]
    public class RolesController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetRoles()
        {
            try
            {
                var listaRoles = RolesRepository.ConsultarRoles();
                return Request.CreateResponse(HttpStatusCode.OK, listaRoles);
            }
            catch (Exception e)
            {
                Console.WriteLine($"An Exception has been caught: {e.Message}");
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage GetRoleByID(int id)
        {
            try
            {
                var rol = RolesRepository.ConsultarRolByID(id);
                if (rol == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
                return Request.CreateResponse(HttpStatusCode.OK, rol);
            }
            catch (Exception e)
            {
                Console.WriteLine($"An Exception has been caught: {e.Message}");
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public HttpResponseMessage CreateRol([FromBody] CrearRolRequest request)
        {
            var rol = RolesRepository.InsertarRol(request.NameRol);
            return Request.CreateResponse(HttpStatusCode.OK, rol);
        }

        [HttpPut]
        [Route("{id}")]
        public HttpResponseMessage UpdateRol([FromUri] int id, [FromBody] ActualizarRolRequest request)
        {
            var rol = RolesRepository.ConsultarRolByID(id);
            if (rol == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            rol.NameRol = request.NameRol;

            RolesRepository.ActualizarRol(id, request.NameRol);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpDelete]
        [Route("{id}")]
        public HttpResponseMessage DeleteRol(int id)
        {
            RolesRepository.EliminarRol(id);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }    
}
