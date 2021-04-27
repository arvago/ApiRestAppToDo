using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrelloApp.DomainModel.Entities;

namespace TrelloApp.OutputAdapter.Repositories
{
    public class StatesRepository
    {
        static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["trello_app"].ToString();
        static MySqlConnection CrearConexion()
        {
            return new MySqlConnection(ConnectionString);
        }

        public static List<State> ConsultarStates()
        {
            using (var connection = CrearConexion())
            {
                return connection.Query<State>("getAll_state", commandType: CommandType.StoredProcedure).AsList();
            }
        }

        public static State ConsultarStateByID(int id)
        {
            using (var connection = CrearConexion())
            {
                var parameters = new DynamicParameters();
                parameters.Add("p_id", id);
                return connection.Query<State>("getById_state", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }
    }
}
