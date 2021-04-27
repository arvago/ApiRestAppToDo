using Dapper;
using System.Linq;
using MySql.Data.MySqlClient;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using TrelloApp.DomainModel.Entities;

namespace TrelloApp.OutputAdapter.Repositories
{
    public static class PersonsRepository
    {
        static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["trello_app"].ToString();
        static MySqlConnection CrearConexion()
        {
            return new MySqlConnection(ConnectionString);
        }

        public static List<Person> ConsultarPersonas()
        {
            using (var connection = CrearConexion())
            {
                return connection.Query<Person>("getAll_person", commandType: CommandType.StoredProcedure).AsList();
            }
        }

        public static Person ConsultarPersonaByID(int id)
        {
            using (var connection = CrearConexion())
            {
                var parameters = new DynamicParameters();
                parameters.Add("p_id", id);
                return connection.Query<Person>("getById_person", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public static Person InsertarPersona(string nombre, int idRol)
        {
            using (var connection = CrearConexion())
            {
                var parameters = new DynamicParameters();
                parameters.Add("p_id", DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("p_RolSalida", dbType: DbType.String, direction: ParameterDirection.Output);
                parameters.Add("p_nombre", nombre);
                parameters.Add("p_rol", idRol);
                connection.Execute("insert_person", parameters, commandType: CommandType.StoredProcedure);

                var id = parameters.Get<int>("p_id");
                string rolSalida = parameters.Get<string>("p_RolSalida");
                return new Person(id, nombre, rolSalida);
            }
        }

        public static Person ActualizarPersona(int id, string nombre, int idRol)
        {
            using (var connection = CrearConexion())
            {
                var parameters = new DynamicParameters();

                parameters.Add("p_RolSalida", dbType: DbType.String, direction: ParameterDirection.Output);
                parameters.Add("p_id", id);
                parameters.Add("p_nombre", nombre);
                parameters.Add("p_rol", idRol);                
                connection.Execute("update_person", parameters, commandType: CommandType.StoredProcedure);

                string rolSalida = parameters.Get<string>("p_RolSalida");

                return new Person(id, nombre, rolSalida);
            }
        }

        public static void EliminarPersona(int id)
        {
            using (var connection = CrearConexion())
            {
                var parameters = new DynamicParameters();

                parameters.Add("p_id", id);
                connection.Execute("delete_person", parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
