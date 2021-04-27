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
    public class TodosRepository
    {
        static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["trello_app"].ToString();
        static MySqlConnection CrearConexion()
        {
            return new MySqlConnection(ConnectionString);
        }

        public static List<ToDo> ConsultarTodos()
        {
            using (var connection = CrearConexion())
            {
                return connection.Query<ToDo>("getAll_toDo", commandType: CommandType.StoredProcedure).AsList();
            }
        }

        public static ToDo ConsultarTodoByID(int id)
        {
            using (var connection = CrearConexion())
            {
                var parameters = new DynamicParameters();
                parameters.Add("p_id", id);
                return connection.Query<ToDo>("getById_toDo", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public static ToDo InsertarTodo(string description, int idStatus, int idUser, DateTime fecha)
        {
            using (var connection = CrearConexion())
            {
                var parameters = new DynamicParameters();
                parameters.Add("p_id", DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("p_statusSalida", dbType: DbType.String, direction: ParameterDirection.Output);
                parameters.Add("p_usuarioSalida", dbType: DbType.String, direction: ParameterDirection.Output);
                parameters.Add("p_idStatus", idStatus);
                parameters.Add("p_idUser", idUser);
                parameters.Add("p_information", description);
                parameters.Add("p_deadline", fecha);
                connection.Execute("insert_toDo", parameters, commandType: CommandType.StoredProcedure);

                var id = parameters.Get<int>("p_id");
                string statusSalida = parameters.Get<string>("p_statusSalida");
                string usuarioSalida = parameters.Get<string>("p_usuarioSalida");
                return new ToDo(id, description, statusSalida, usuarioSalida, fecha);
            }
        }

        public static ToDo ActualizarTodo(int id, string description, int idStatus, int idUser, DateTime fecha)
        {
            using (var connection = CrearConexion())
            {
                var parameters = new DynamicParameters();

                parameters.Add("p_statusSalida", dbType: DbType.String, direction: ParameterDirection.Output);
                parameters.Add("p_usuarioSalida", dbType: DbType.String, direction: ParameterDirection.Output);
                parameters.Add("p_id", id);
                parameters.Add("p_idStatus", idStatus);
                parameters.Add("p_idUser", idUser);
                parameters.Add("p_information", description);
                parameters.Add("p_deadline", fecha);
                connection.Execute("update_toDo", parameters, commandType: CommandType.StoredProcedure);

                string statusSalida = parameters.Get<string>("p_statusSalida");
                string usuarioSalida = parameters.Get<string>("p_usuarioSalida");

                return new ToDo(id, description, statusSalida, usuarioSalida, fecha);
            }
        }

        public static void EliminarTodo(int id)
        {
            using (var connection = CrearConexion())
            {
                var parameters = new DynamicParameters();

                parameters.Add("p_id", id);
                connection.Execute("delete_toDo", parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
