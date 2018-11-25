using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Entities;
using UsersAndAwards.DAL.Interfaces;

namespace UsersAndAwards.DAL.Repositories
{
    public class UserDb : IRepository<User>
    {
        //private SqlConnection _connection;
        private string _connectionString;

        public UserDb(string connectionString)
        {
            _connectionString = connectionString;
            
        }
        public IEnumerable<User> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlConnection awardConnection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand())
            using (var getAwardsCommand = new SqlCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.GetUsers";
                command.Connection = connection;
                getAwardsCommand.CommandType = CommandType.StoredProcedure;
                getAwardsCommand.CommandText = "dbo.GetUserRewards";
                getAwardsCommand.Connection = awardConnection;
                getAwardsCommand.Parameters.Add("@userId", SqlDbType.Int);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int id = (int)reader["Id"];
                    var name = (string)reader["FirstName"];
                    var lastName = (string)reader["LastName"];
                    DateTime brithdate = (DateTime)reader["Birthdate"];
                    getAwardsCommand.Parameters["@userId"].Value = id;
                    List<Award> awards = new List<Award>();
                    awardConnection.Open();
                    var awardReader = getAwardsCommand.ExecuteReader();
                    while (awardReader.Read())
                    {
                        int awardId = (int)awardReader["Id"];
                        string title = (string)awardReader["Name"];
                        string decription = (string)awardReader["Description"];
                        awards.Add(new Award()
                        {
                            Description = decription,
                            Id = awardId,
                            Title = title
                        });
                    }
                    awardConnection.Close();
                    yield return new User()
                    {
                        FirstName = name,
                        LastName = lastName,
                        BirthDate = brithdate,
                        Id = id,
                         Awards = awards
                    };
                }
            }
        }

        public User Get(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand())
            using (var getAwardsCommand = new SqlCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.GetUser";
                command.Connection = connection;
                connection.Open();

                command.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                var ret = command.ExecuteReader();
                if (!ret.Read())
                {
                    return null;
                }

                var name = (string)ret["FirstName"];
                var lastName = (string)ret["LastName"];
                var brithdate = (DateTime)ret["Birthdate"];
                connection.Close();
                getAwardsCommand.CommandType = CommandType.StoredProcedure;
                getAwardsCommand.CommandText = "dbo.GetUserRewards";
                getAwardsCommand.Connection = connection;
                connection.Open();
                getAwardsCommand.Parameters.Add("@userId", SqlDbType.Int).Value = id;
                List<Award> awards = new List<Award>();
                var reader = getAwardsCommand.ExecuteReader();
                while (reader.Read())
                {
                    int awardId = (int)reader["Id"];
                    string title = (string)reader["Name"];
                    string decription = (string)reader["Description"];
                    awards.Add(new Award()
                    {
                        Description = decription,
                        Id = awardId,
                        Title = title
                    });
                }

                return new User
                {
                    FirstName = name,
                    LastName = lastName,
                    BirthDate = brithdate,
                    Id = id,
                    Awards = awards
                };
            }
        }

        public IEnumerable<User> Find(Func<User, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public void Create(User item)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.InsertUser";
                command.Connection = connection;
                connection.Open();

                command.Parameters.Add("@firstName", SqlDbType.NVarChar).Value = item.FirstName;
                command.Parameters.Add("@lastName", SqlDbType.NVarChar).Value = item.LastName;
                command.Parameters.Add("@birthdate", SqlDbType.DateTime2).Value = item.BirthDate;
                command.Parameters.AddWithValue("@awardIds", CreateDataTable(item.Awards));
                item.Id = (int)command.ExecuteScalar();

            }
        }
        private static DataTable CreateDataTable(IEnumerable<Award> awards)
        {
            DataTable table = new DataTable();
            table.Columns.Add("AwardId", typeof(int));
            foreach (var a in awards)
            {
                table.Rows.Add(a.Id);
            }
            return table;
        }
        public void Update(User item)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.UpdateUser";
                command.Connection = connection;
                connection.Open();

                command.Parameters.Add("@Id", SqlDbType.Int).Value = item.Id;
                command.Parameters.Add("@firstName", SqlDbType.NVarChar).Value = item.FirstName;
                command.Parameters.Add("@lastName", SqlDbType.NVarChar).Value = item.LastName;
                command.Parameters.Add("@birthdate", SqlDbType.DateTime2).Value = item.BirthDate;
                command.Parameters.AddWithValue("@awardIds", CreateDataTable(item.Awards));
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.DeleteUser";
                command.Connection = connection;
                connection.Open();

                command.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                command.ExecuteNonQuery();
            }
        }
    }
}