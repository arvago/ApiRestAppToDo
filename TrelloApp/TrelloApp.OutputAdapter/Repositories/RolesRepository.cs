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
    public class RolesRepository
    {
        static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["trello_app"].ToString();
        static MySqlConnection CrearConexion()
        {
            return new MySqlConnection(ConnectionString);
        }

        public static List<Rol> ConsultarRoles()
        {
            using (var connection = CrearConexion())
            {
                return connection.Query<Rol>("getAll_rol", commandType: CommandType.StoredProcedure).AsList();
            }
        }

        public static Rol ConsultarRolByID(int id)
        {
            using (var connection = CrearConexion())
            {
                var parameters = new DynamicParameters();
                parameters.Add("p_id", id);
                return connection.Query<Rol>("getById_rol", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public static Rol InsertarRol(string nombre)
        {
            using (var connection = CrearConexion())
            {
                var parameters = new DynamicParameters();
                parameters.Add("p_id", DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("p_nombre", nombre);
                connection.Execute("insert_rol", parameters, commandType: CommandType.StoredProcedure);

                var id = parameters.Get<int>("p_id");
                return new Rol(id, nombre);
            }
        }

        public static Rol ActualizarRol(int id, string nombre)
        {
            using (var connection = CrearConexion())
            {
                var parameters = new DynamicParameters();

                parameters.Add("p_id", id);
                parameters.Add("p_nombre", nombre);
                connection.Execute("update_rol", parameters, commandType: CommandType.StoredProcedure);

                return new Rol(id, nombre);
            }
        }

        public static void EliminarRol(int id)
        {
            using (var connection = CrearConexion())
            {
                var parameters = new DynamicParameters();

                parameters.Add("p_id", id);
                connection.Execute("delete_rol", parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
