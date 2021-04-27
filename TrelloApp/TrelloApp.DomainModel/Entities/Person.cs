using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrelloApp.DomainModel.Entities
{
    public class Person
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string NameRol { get; set; }

        public Person(int ID, string Nombre, string NameRol)
        {
            this.ID = ID;
            this.Nombre = Nombre;
            this.NameRol = NameRol;
        }
    }
}
