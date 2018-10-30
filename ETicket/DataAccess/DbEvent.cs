using Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DbEvent : ICRUD
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ETicketDb"].ConnectionString;

        // Create Event 
        public void Create(Object obj)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    Event myEvent = (Event)obj;
                    command.CommandText = "Insert into Event (Title, Description, Gate, GateOpens, StartTime, Date, AvailableTickets, TicketPrice) values (@Title, @Description, @Gate, @GateOpens, @StartTime, @Date, @AvailableTickets, @TicketPrice)";
                    command.Parameters.AddWithValue("Title", myEvent.Title);
                    command.Parameters.AddWithValue("Description", myEvent.Description);
                    command.Parameters.AddWithValue("Gate", myEvent.Gate);
                    command.Parameters.AddWithValue("GateOpens", myEvent.GateOpens);
                    command.Parameters.AddWithValue("StartTime", myEvent.StartTime);
                    command.Parameters.AddWithValue("Date", myEvent.Date);
                    command.Parameters.AddWithValue("AvailableTickets", myEvent.AvailableTickets);
                    command.Parameters.AddWithValue("TicketPrice", myEvent.TicketPrice);
                    command.ExecuteNonQuery();
                }
            }
        }


        // Get Event
        public object Get(int id = 0)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Event newEvent = null;
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Select * from Event where EventId = @id";
                    SqlParameter EventId = command.Parameters.Add("@id", SqlDbType.Int);
                    EventId.Value = id;

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        newEvent = new Event
                        {
                            EventId = reader.GetInt32(reader.GetOrdinal("EventId")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            Description = reader.GetString(reader.GetOrdinal("Description")),
                            Gate = reader.GetString(reader.GetOrdinal("Gate")),
                            GateOpens = reader.GetDateTime(reader.GetOrdinal("GateOpens")),
                            StartTime = reader.GetDateTime(reader.GetOrdinal("StartTime")),
                            Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                            AvailableTickets = reader.GetInt32(reader.GetOrdinal("AvailableTickets")),
                            TicketPrice = reader.GetDecimal(reader.GetOrdinal("TicketPrice"))
                        };
                    }
                    return newEvent;
                }
            }
        }


        // Delete Event 
        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Delete from Event where EventId = @id";
                    SqlParameter EventId = command.Parameters.Add("@id", SqlDbType.Int);
                    EventId.Value = id;

                    command.ExecuteNonQuery();
                }
            }
        }

        // Update Event
        public void Update(int id, Object obj)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Update Event SET Title = '', Description = '', Gate = '', GateOpens = '', StartTime = '', Date = '', AvailableTickets = '', TicketPrice = '' where EventId = @id";
                    SqlParameter EventId = command.Parameters.Add("@id", SqlDbType.Int);
                    EventId.Value = id;
              
                    command.ExecuteNonQuery();
                }
            }
        }


    }

}
