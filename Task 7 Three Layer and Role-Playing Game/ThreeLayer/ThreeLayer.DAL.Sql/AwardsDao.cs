﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using ThreeLayer.Common.Entities;
using ThreeLayer.DAL.Contracts;

namespace ThreeLayer.DAL.Sql
{
    public class AwardsDao : IEntityWithIdDao<Award>
    {
        private readonly string _connectionString;

        public AwardsDao()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
        }

        public event EventHandler<Award> Removed;

        public int Add(Award entity)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "dbo.AddAward";

                command.Parameters.Add(new SqlParameter("Title", entity.Title));

                connection.Open();

                command.ExecuteScalar();

                return GetAll().Max(item => item.Id);
            }
        }

        public IEnumerable<Award> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var query = "SELECT * FROM Awards";

                var command = new SqlCommand(query, connection);

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

        public bool RemoveById(int id)
        {
            if (!GetAll().Any(item => item.Id == id))
                return false;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "dbo.RemoveAwardById";

                command.Parameters.Add(new SqlParameter("Id", id));

                connection.Open();

                command.ExecuteScalar();

                Removed?.Invoke(this, GetAll().FirstOrDefault(item => item.Id == id));

                return true;
            }
        }

        public void Update(Award entity)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "dbo.UpdateAward";

                command.Parameters.Add(new SqlParameter("Id", entity.Id));
                command.Parameters.Add(new SqlParameter("Title", entity.Title));

                connection.Open();

                command.ExecuteScalar();
            }
        }
    }
}
