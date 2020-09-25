using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using ThreeLayer.Common.Entities;
using ThreeLayer.DAL.Contracts;

namespace ThreeLayer.DAL.Sql
{
    public class AuthUserDataDao : IEntityWithIdDao<AuthUserData>
    {
        private readonly string _connectionString;

        public AuthUserDataDao()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
        }

        public event EventHandler<AuthUserData> Removed;

        public int Add(AuthUserData entity)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "dbo.AddAuthUserData";

                command.Parameters.Add(new SqlParameter("Password", entity.Password));
                command.Parameters.Add(new SqlParameter("UserId", entity.UserId));

                connection.Open();

                command.ExecuteScalar();

                return GetAll().Max(item => item.Id);
            }
        }

        public IEnumerable<AuthUserData> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var query = "SELECT * FROM AuthUserData";

                var command = new SqlCommand(query, connection);

                connection.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    yield return new AuthUserData()
                    {
                        Id = (int)reader["Id"],
                        Password = reader["Password"] as string,
                        UserId = (int)reader["UserId"]
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
                command.CommandText = "dbo.RemoveAuthUserDataById";

                command.Parameters.Add(new SqlParameter("Id", id));

                connection.Open();

                command.ExecuteScalar();

                Removed?.Invoke(this, GetAll().FirstOrDefault(item => item.Id == id));

                return true;
            }
        }

        public void Update(AuthUserData entity)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "dbo.UpdateAuthUserData";

                command.Parameters.Add(new SqlParameter("Id", entity.Id));
                command.Parameters.Add(new SqlParameter("Password", entity.Password));
                command.Parameters.Add(new SqlParameter("UserId", entity.UserId));

                connection.Open();

                command.ExecuteScalar();
            }
        }
    }
}
