using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using TrelloApp.Api.Request;
using TrelloApp.OutputAdapter.Repositories;

namespace TrelloApp.Api.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/todos")]
    public class TodosController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetTodos()
        {
            try
            {
                var listaTodos = TodosRepository.ConsultarTodos();
                return Request.CreateResponse(HttpStatusCode.OK, listaTodos);
            }
            catch (Exception e)
            {
                Console.WriteLine($"An Exception has been caught: {e.Message}");
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage GetTodoByID(int id)
        {
            try
            {
                var toDo = TodosRepository.ConsultarTodoByID(id);
                if (toDo == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
                return Request.CreateResponse(HttpStatusCode.OK, toDo);
            }
            catch (Exception e)
            {
                Console.WriteLine($"An Exception has been caught: {e.Message}");
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public HttpResponseMessage CreateTodo([FromBody] CrearTodoRequest request)
        {
            var toDo = TodosRepository.InsertarTodo(request.Descripcion, request.idStatus, request.idUser, request.Fecha);
            return Request.CreateResponse(HttpStatusCode.OK, toDo);
        }

        [HttpPut]
        [Route("{id}")]
        public HttpResponseMessage UpdateTodo([FromUri] int id, [FromBody] ActualizarTodoRequest request)
        {
            var toDo = TodosRepository.ConsultarTodoByID(id);
            if (toDo == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            TodosRepository.ActualizarTodo(id, request.Descripcion, request.idStatus, request.idUser, request.Fecha);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpDelete]
        [Route("{id}")]
        public HttpResponseMessage DeleteTodo(int id)
        {
            TodosRepository.EliminarTodo(id);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
