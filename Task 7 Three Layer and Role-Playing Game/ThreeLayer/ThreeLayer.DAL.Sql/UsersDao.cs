using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using ThreeLayer.Common.Entities;
using ThreeLayer.DAL.Contracts;

namespace ThreeLayer.DAL.Sql
{
    public class UsersDao : IEntityWithIdDao<User>
    {
        private readonly string _connectionString;

        public UsersDao()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
        }

        public event EventHandler<User> Removed;

        public int Add(User entity)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "dbo.AddUser";

                command.Parameters.Add(new SqlParameter("Name", entity.Name));
                command.Parameters.Add(new SqlParameter("DateOfBirth", entity.DateOfBirth));
                command.Parameters.Add(new SqlParameter("AuthUserDataId", entity.AuthUserDataId));

                connection.Open();
                command.ExecuteScalar();

                return GetAll().Max(item => item.Id);
            }
        }

        public IEnumerable<User> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var query = "SELECT * FROM Users";

                var command = new SqlCommand(query, connection);

                connection.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    yield return new User()
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"] as string,
                        DateOfBirth = (DateTime)reader["DateOfBirth"],
                        AuthUserDataId = (int)reader["AuthUserDataId"]
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
                command.CommandText = "dbo.RemoveUserById";

                command.Parameters.Add(new SqlParameter("Id", id));

                connection.Open();

                command.ExecuteScalar();

                Removed?.Invoke(this, GetAll().FirstOrDefault(item => item.Id == id));

                return true;
            }
        }

        public void Update(User entity)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "dbo.UpdateUser";

                command.Parameters.Add(new SqlParameter("Id", entity.Id));
                command.Parameters.Add(new SqlParameter("Name", entity.Name));
                command.Parameters.Add(new SqlParameter("DateOfBirth", entity.DateOfBirth));

                connection.Open();

                command.ExecuteScalar();
            }
        }
    }
}
