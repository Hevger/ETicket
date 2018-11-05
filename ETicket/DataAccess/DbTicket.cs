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
    public class DbTicket : ICRUD
    {
        string connectionString = ConfigurationManager.ConnectionStrings["Kraka"].ConnectionString;

        // Create Ticket
        public void Create(object obj)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    Ticket myTicket = (Ticket)obj;
                    command.CommandText = "Insert into Ticket (SeatId, EventId, CustomerId) values (@SeatId, @EventId, @CustomerId)";
                    command.Parameters.AddWithValue("SeatId", myTicket.SeatId);
                    command.Parameters.AddWithValue("EventId", myTicket.EventId);
                    command.Parameters.AddWithValue("CustomerId", myTicket.CustomerId);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Delete Ticket
        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Delete from Ticket where TicketId = @id";
                    command.Parameters.AddWithValue("id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Get Ticket
        public object Get(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Ticket newTicket = null;
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Select * from Ticket where TicketId = @id";
                    command.Parameters.AddWithValue("id", id);

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        newTicket = new Ticket
                        {
                            TicketId = reader.GetInt32(reader.GetOrdinal("TicketId")),
                            SeatId = reader.GetInt32(reader.GetOrdinal("SeatId")),
                            EventId = reader.GetInt32(reader.GetOrdinal("EventId")),
                            CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerId"))
                        };
                    }
                    return newTicket;
                }
            }
        }

        // Update
        public void Update(object obj)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    Ticket myTicket = (Ticket)obj;
                    command.CommandText = "Update Ticket SET SeatId = @SeatId, EventId = @EventId, CustomerId = @CustomerId where TicketId = @TicketId";
                    command.Parameters.AddWithValue("TicketId", myTicket.TicketId);
                    command.Parameters.AddWithValue("SeatId", myTicket.SeatId);
                    command.Parameters.AddWithValue("EventId", myTicket.EventId);
                    command.Parameters.AddWithValue("CustomerId", myTicket.CustomerId);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Get All Tickets
        public List<Object> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                List<Object> tickets = new List<Object>();
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Select * from Ticket";

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Ticket newTicket = new Ticket
                        {
                            TicketId = reader.GetInt32(reader.GetOrdinal("TicketId")),
                            SeatId = reader.GetInt32(reader.GetOrdinal("SeatId")),
                            EventId = reader.GetInt32(reader.GetOrdinal("EventId")),
                            CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerId"))
                        };
                        tickets.Add(newTicket);
                    }
                    return tickets;
                }
            }
        }


    }
}
