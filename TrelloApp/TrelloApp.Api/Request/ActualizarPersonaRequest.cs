using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrelloApp.Api.Request
{
    public class ActualizarPersonaRequest
    {
        public string Nombre { get; set; }
        public int idRol { get; set; }
    }
}