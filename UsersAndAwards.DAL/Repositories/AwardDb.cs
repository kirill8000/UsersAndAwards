using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Entities;
using UsersAndAwards.DAL.Interfaces;

namespace UsersAndAwards.DAL.Repositories
{
    public class AwardDb : IRepository<Award>
    {
        private string _connectionString;

        public AwardDb(string connectionString)
        {
            _connectionString = connectionString;

        }
        public IEnumerable<Award> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.GetAwards";
                command.Connection = connection;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int id = (int)reader["Id"];
                    string name = (string)reader["Name"];
                    string description = (string)reader["Description"];

                    yield return new Award()
                    {
                        Description = description,
                        Title = name,
                        Id = id
                    };
                }
            }
        }

        public Award Get(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.GetAward";
                command.Connection = connection;
                connection.Open();

                command.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
           
                    string name = (string)reader["Name"];
                    string description = (string)reader["Description"];

                    return new Award()
                    {
                        Description = description,
                        Title = name,
                        Id = id
                    };
                }
            }

            return null;
        }

        public IEnumerable<Award> Find(Func<Award, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public void Create(Award item)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.InsertAward";
                command.Connection = connection;
                connection.Open();

                command.Parameters.Add("@name", SqlDbType.NVarChar).Value = item.Title;
                command.Parameters.Add("@description", SqlDbType.NVarChar).Value = item.Description;

                item.Id = (int)command.ExecuteScalar();
            }
        }

        public void Update(Award item)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.UpdateAward";
                command.Connection = connection;
                connection.Open();

                command.Parameters.Add("@id", SqlDbType.Int).Value = item.Id;
                command.Parameters.Add("@name", SqlDbType.NVarChar).Value = item.Title;
                command.Parameters.Add("@description", SqlDbType.NVarChar).Value = item.Description;

                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.DeleteAward";
                command.Connection = connection;
                connection.Open();

                command.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                command.ExecuteNonQuery();
            }
        }
    }
}