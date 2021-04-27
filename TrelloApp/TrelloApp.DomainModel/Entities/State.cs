using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrelloApp.DomainModel.Entities
{
    public class State
    {
        public int ID { get; set; }
        public string Status { get; set; }

        public State(int ID, string Status)
        {
            this.ID = ID;
            this.Status = Status;
        }
    }
}
