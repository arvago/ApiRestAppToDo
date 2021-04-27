using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using TrelloApp.Api.Request;
using TrelloApp.OutputAdapter.Repositories;

namespace TrelloApp.Api.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/persons")]
    public class PersonsController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetPersons()
        {
            try
            {
                var listaPersons = PersonsRepository.ConsultarPersonas();
                return Request.CreateResponse(HttpStatusCode.OK, listaPersons);
            }
            catch (Exception e)
            {
                Console.WriteLine($"An Exception has been caught: {e.Message}");
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage GetPersonByID(int id)
        {
            try
            {
                var persona = PersonsRepository.ConsultarPersonaByID(id);
                if (persona == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
                return Request.CreateResponse(HttpStatusCode.OK, persona);
            }
            catch (Exception e)
            {
                Console.WriteLine($"An Exception has been caught: {e.Message}");
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public HttpResponseMessage CreatePerson([FromBody] CrearPersonaRequest request)
        {
            var persona = PersonsRepository.InsertarPersona(request.Nombre, request.idRol);
            return Request.CreateResponse(HttpStatusCode.OK, persona);
        }

        [HttpPut]
        [Route("{id}")]
        public HttpResponseMessage UpdatePerson([FromUri] int id, [FromBody] ActualizarPersonaRequest request)
        {
            var alumno = PersonsRepository.ConsultarPersonaByID(id);
            if (alumno == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            PersonsRepository.ActualizarPersona(id, request.Nombre, request.idRol);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpDelete]
        [Route("{id}")]
        public HttpResponseMessage DeleteAlumno(int id)
        {
            PersonsRepository.EliminarPersona(id);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
