using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using ThreeLayer.Common.Entities;
using ThreeLayer.DAL.Contracts;

namespace ThreeLayer.DAL.Sql
{
    public class UserRoleAssociationsDao : IAssociationsDao<User, Role>
    {
        private readonly string _connectionString;

        public UserRoleAssociationsDao()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
        }

        public bool Bind(int entityId, int associatedEntityId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "dbo.AddUserRoleAssociation";

                command.Parameters.Add(new SqlParameter("UserId", entityId));
                command.Parameters.Add(new SqlParameter("RoleId", associatedEntityId));

                connection.Open();

                try
                {
                    command.ExecuteScalar();
                }
                catch (SqlException)
                {
                    return false;
                }

                return true;
            }
        }

        public IEnumerable<Role> GetAssociatedEntities(int entityId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "dbo.GetUserRoles";

                command.Parameters.Add(new SqlParameter("UserId", entityId));

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

        public bool UnBind(int entityId, int associatedEntityId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "dbo.RemoveUserRoleAssociation";

                command.Parameters.Add(new SqlParameter("UserId", entityId));
                command.Parameters.Add(new SqlParameter("RoleId", associatedEntityId));

                connection.Open();

                try
                {
                    command.ExecuteScalar();
                }
                catch (SqlException)
                {
                    return false;
                }

                return true;
            }
        }
    }
}
