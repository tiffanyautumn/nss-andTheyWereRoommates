using Microsoft.Data.SqlClient;
using Roommates.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace Roommates.Repositories
{
    public class RoommateRepository : BaseRepository

    {
        public RoommateRepository(string connectionString) : base(connectionString){ }

        public Roommate GetById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                    using(SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT rm.Id, rm.FirstName, rm.RentPortion, r.Name, rm.RoomId
                                        FROM Roommate rm
                                        JOIN Room r ON r.Id = rm.RoomId 
                                        WHERE rm.Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        Roommate roommate = null;
                        {
                            if(reader.Read())
                            {
                                roommate = new Roommate()
                                {
                                    Id = id,
                                    FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                    RentPortion = reader.GetInt32(reader.GetOrdinal("RentPortion")),
                                    Room = new Room
                                    {
                                        Id = reader.GetInt32(reader.GetOrdinal("RoomId")),
                                        Name = reader.GetString(reader.GetOrdinal("Name"))
                                     }
                                };


                            }
                            return roommate;
                        }
                    }
                    

                   
                }
            }
        }
        public List<Roommate> GetAll()
        {
            using(SqlConnection conn = Connection)
            {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Id, FirstName, RentPortion
                                        FROM Roommate";
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Roommate> roommates = new List<Roommate>();
                        while(reader.Read())
                        {
                            Roommate roommate = new Roommate()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                RentPortion = reader.GetInt32(reader.GetOrdinal("RentPortion"))
                            };
                            roommates.Add(roommate);
                            
                        }
                        return roommates;
                    }
                }
            }
        }
    }
}
