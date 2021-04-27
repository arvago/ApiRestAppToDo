using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using TrelloApp.OutputAdapter.Repositories;

namespace TrelloApp.Api.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/states")]
    public class StatesController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetStates()
        {
            try
            {
                var listaStates = StatesRepository.ConsultarStates();
                return Request.CreateResponse(HttpStatusCode.OK, listaStates);
            }
            catch (Exception e)
            {
                Console.WriteLine($"An Exception has been caught: {e.Message}");
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage GetStateByID(int id)
        {
            try
            {
                var state = StatesRepository.ConsultarStateByID(id);
                if (state == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
                return Request.CreateResponse(HttpStatusCode.OK, state);
            }
            catch (Exception e)
            {
                Console.WriteLine($"An Exception has been caught: {e.Message}");
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}
