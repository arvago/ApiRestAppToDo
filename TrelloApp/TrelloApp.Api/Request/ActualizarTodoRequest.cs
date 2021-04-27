using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrelloApp.Api.Request
{
    public class ActualizarTodoRequest
    {
        public string Descripcion { get; set; }
        public int idStatus { get; set; }
        public int idUser { get; set; }
        public DateTime Fecha { get; set; }
    }
}