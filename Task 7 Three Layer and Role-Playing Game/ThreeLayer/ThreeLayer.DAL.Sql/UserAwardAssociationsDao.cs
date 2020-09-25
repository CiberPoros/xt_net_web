using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeLayer.Common.Entities;
using ThreeLayer.DAL.Contracts;

namespace ThreeLayer.DAL.Sql
{
    public class UserAwardAssociationsDao : IAssociationsDao<User, Award>
    {
        private readonly string _connectionString;

        public UserAwardAssociationsDao()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
        }

        public bool Bind(int entityId, int associatedEntityId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "dbo.AddUserAwardAssociation";

                command.Parameters.Add(new SqlParameter("UserId", entityId));
                command.Parameters.Add(new SqlParameter("AwardId", associatedEntityId));

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

        public IEnumerable<Award> GetAssociatedEntities(int entityId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "dbo.GetUserAwards";

                command.Parameters.Add(new SqlParameter("UserId", entityId));

                connection.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    yield return new Award()
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
                command.CommandText = "dbo.RemoveUserAwardAssociation";

                command.Parameters.Add(new SqlParameter("UserId", entityId));
                command.Parameters.Add(new SqlParameter("AwardId", associatedEntityId));

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
