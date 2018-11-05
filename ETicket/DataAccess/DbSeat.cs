using Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DbSeat : ICRUD
    {
        string connectionString = ConfigurationManager.ConnectionStrings["Kraka"].ConnectionString;

        // Create Seat
        public void Create(object obj)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    Seat mySeat = (Seat)obj;
                    command.CommandText = "Insert into Seat (SeatNumber, EventId, Available) values (@SeatNumber, @EventId, @Available)";
                    command.Parameters.AddWithValue("SeatNumber", mySeat.SeatNumber);
                    command.Parameters.AddWithValue("EventId", mySeat.EventId);
                    command.Parameters.AddWithValue("Available", mySeat.Available);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Delete Seat
        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Delete from Seat where SeatId = @id";
                    command.Parameters.AddWithValue("id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Get Seat
        public object Get(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Seat newSeat = null;
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Select * from Seat where SeatId = @id";
                    command.Parameters.AddWithValue("id", id);

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        newSeat = new Seat
                        {
                            SeatId = reader.GetInt32(reader.GetOrdinal("SeatId")),
                            SeatNumber = reader.GetInt32(reader.GetOrdinal("SeatNumber")),
                            EventId = reader.GetInt32(reader.GetOrdinal("EventId")),
                            Available = reader.GetBoolean(reader.GetOrdinal("Available"))
                        };
                    }
                    return newSeat;
                }
            }
        }

        // Update Seat
        public void Update(object obj)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    Seat mySeat = (Seat)obj;
                    command.CommandText = "Update Seat SET SeatNumber = @SeatNumber, EventId = @EventId, Available = @Available where SeatId = @SeatId";
                    command.Parameters.AddWithValue("SeatId", mySeat.SeatId);
                    command.Parameters.AddWithValue("SeatNumber", mySeat.SeatNumber);
                    command.Parameters.AddWithValue("EventId", mySeat.EventId);
                    command.Parameters.AddWithValue("Available", mySeat.Available);
                    command.ExecuteNonQuery();
                }
            }
        }


        // Get All Seats
        public List<Object> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                List<Object> seats = new List<Object>();
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Select * from Seat";

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Seat newSeat = new Seat
                        {
                            SeatId = reader.GetInt32(reader.GetOrdinal("SeatId")),
                            SeatNumber = reader.GetInt32(reader.GetOrdinal("SeatNumber")),
                            EventId = reader.GetInt32(reader.GetOrdinal("EventId")),
                            Available = reader.GetBoolean(reader.GetOrdinal("Available"))
                        };
                        seats.Add(newSeat);
                    }
                    return seats;
                }
            }
        }


    }
}
