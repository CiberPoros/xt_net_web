using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeLayer.Common.Entities;
using ThreeLayer.DAL.Contracts;

namespace ThreeLayer.DAL.Sql
{
    public class RolesDao : IEntityWithIdDao<Role>
    {
        private readonly string _connectionString;

        public RolesDao()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
        }

        public event EventHandler<Role> Removed;

        public int Add(Role entity)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "dbo.AddRole";

                command.Parameters.Add(new SqlParameter("Title", entity.Title));

                connection.Open();

                command.ExecuteScalar();

                return GetAll().Max(item => item.Id);
            }
        }

        public IEnumerable<Role> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var query = "SELECT * FROM Roles";

                var command = new SqlCommand(query, connection);

                connection.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    yield return new Role()
                    {
                        Id = (int)reader["Id"],
                        Title = reader["Title"] as string,
                    };
                }
            }
        }

        public bool RemoveById(int id)
        {
            if (!GetAll().Any(item => item.Id == id))
                return false;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "dbo.RemoveRoleById";

                command.Parameters.Add(new SqlParameter("Id", id));

                connection.Open();

                command.ExecuteScalar();

                Removed?.Invoke(this, GetAll().FirstOrDefault(item => item.Id == id));

                return true;
            }
        }

        public void Update(Role entity)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "dbo.UpdateRole";

                command.Parameters.Add(new SqlParameter("Id", entity.Id));
                command.Parameters.Add(new SqlParameter("Title", entity.Title));

                connection.Open();

                command.ExecuteScalar();
            }
        }
    }
}
