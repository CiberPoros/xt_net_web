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
    public class AwardUserAssociationsDao : IAssociationsDao<Award, User>
    {
        private readonly string _connectionString;
        private readonly IAssociationsDao<User, Award> _inverseDao; // TODO: Костыль? :c

        public AwardUserAssociationsDao(IAssociationsDao<User, Award> associationsDao)
        {
            _connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
            _inverseDao = associationsDao ?? throw new ArgumentNullException(nameof(associationsDao));
        }

        public bool Bind(int entityId, int associatedEntityId) => _inverseDao.Bind(associatedEntityId, entityId);
        public IEnumerable<User> GetAssociatedEntities(int entityId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "dbo.GetAwardOwners";

                command.Parameters.Add(new SqlParameter("AwardId", entityId));

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
        public bool UnBind(int entityId, int associatedEntityId) => _inverseDao.UnBind(associatedEntityId, entityId);
    }
}
