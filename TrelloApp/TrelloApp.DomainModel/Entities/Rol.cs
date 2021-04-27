using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrelloApp.DomainModel.Entities
{
    public class Rol
    {
        public int ID { get; set; }
        public string  NameRol{ get; set; }

        public Rol(int ID, string NameRol)
        {
            this.ID = ID;
            this.NameRol = NameRol;
        }
    }
}
