using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrelloApp.DomainModel.Entities
{
    public class ToDo
    {
        public int ID { get; set; }
        public string Status { get; set; }
        public string Usuario { get; set; }
        public string Descripcion { get; set; }
        public DateTime DeadLine { get; set; }

        public ToDo(int ID, string Descripcion, string Status, string Usuario, DateTime DeadLine)
        {
            this.ID = ID;
            this.Status = Status;
            this.Usuario = Usuario;
            this.Descripcion = Descripcion;
            this.DeadLine = DeadLine;
        }
    }
}
