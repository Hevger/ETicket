using Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DataAccess
{
    public class DbEvent : ICRUD
    {
        string connectionString = ConfigurationManager.ConnectionStrings["Kraka"].ConnectionString;
        DbOrder dbOrder = new DbOrder();
        DbTicket dbTicket = new DbTicket();

        public DbEvent()
        {

        }
        // Create Event 
        public int Create(Object obj)
        {
            int insertedId;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    Event myEvent = (Event)obj;
                    command.CommandText = "Insert into Event (Title, Description, Gate, GateOpens, StartTime, Date, AvailableTickets, TicketPrice) values (@Title, @Description, @Gate, @GateOpens, @StartTime, @Date, @AvailableTickets, @TicketPrice); SELECT SCOPE_IDENTITY()";
                    command.Parameters.AddWithValue("Title", myEvent.Title);
                    command.Parameters.AddWithValue("Description", myEvent.Description);
                    command.Parameters.AddWithValue("Gate", myEvent.Gate);
                    command.Parameters.AddWithValue("GateOpens", myEvent.GateOpens);
                    command.Parameters.AddWithValue("StartTime", myEvent.StartTime);
                    command.Parameters.AddWithValue("Date", myEvent.Date);
                    command.Parameters.AddWithValue("AvailableTickets", myEvent.AvailableTickets);
                    command.Parameters.AddWithValue("TicketPrice", myEvent.TicketPrice);
                    insertedId = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            return insertedId;
        }

        public object Get(int id = 0)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                return Get(id, connection);
            }
        }
        // Get Event
        public object Get(int id, SqlConnection connection)
        {

            Event newEvent = null;
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            using (SqlCommand command = new SqlCommand("", connection))
            {
                command.CommandText = "Select * from Event where EventId = @id";
                //SqlParameter EventId = command.Parameters.Add("@id", SqlDbType.Int);
                //EventId.Value = id;
                command.Parameters.AddWithValue("id", id);

                command.CommandTimeout = 5;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
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
                }
                reader.Close();
                return newEvent;
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
                    //SqlParameter EventId = command.Parameters.Add("@id", SqlDbType.Int);
                    //EventId.Value = id;

                    SqlCommand listAllOrders = connection.CreateCommand();
                    SqlCommand deleteOrderItems = connection.CreateCommand();
                    SqlCommand deleteTicketAndSeat = connection.CreateCommand();
                    SqlCommand deleteOrder = connection.CreateCommand();


                    listAllOrders.CommandText = "Select * from Orders where EventId = @id;";
                    listAllOrders.Parameters.AddWithValue("id", id);

                    List<Order> orders = dbOrder.GetAll().Cast<Order>().ToList();

                    foreach (var order in orders)
                    {
                        deleteOrderItems.CommandText = "Delete from OrderItems where OrderId = @OrderId";
                        deleteOrderItems.Parameters.AddWithValue("OrderId", order.OrderId);
                        deleteOrderItems.ExecuteNonQuery();
                        deleteOrderItems.Parameters.Clear();
                    }

                    List<Ticket> tickets = dbTicket.GetAll().Cast<Ticket>().ToList();

                    foreach (var ticket in tickets)
                    {
                        deleteTicketAndSeat.CommandText = "Delete from Ticket where EventId = @EventId; Delete from Seat where EventId = @EventId";
                        deleteTicketAndSeat.Parameters.AddWithValue("EventId", id);
                        deleteTicketAndSeat.ExecuteNonQuery();
                        deleteTicketAndSeat.Parameters.Clear();
                    }

                    foreach (var order in orders)
                    {
                        deleteOrder.CommandText = "Delete from Orders where OrderId = @OrderId";
                        deleteOrder.Parameters.AddWithValue("OrderId", order.OrderId);
                        deleteOrder.ExecuteNonQuery();
                        deleteOrder.Parameters.Clear();
                    }


                    command.Parameters.AddWithValue("id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Update Event
        public void Update(Object obj)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    Event myEvent = (Event)obj;
                    command.CommandText = "Update Event SET Title = @title, Description = @Description, Gate = @Gate, GateOpens = @GateOpens, StartTime = @StartTime, Date = @Date, AvailableTickets = @AvailableTickets, TicketPrice = @TicketPrice where EventId = @id";
                    //SqlParameter EventId = command.Parameters.Add("@id", SqlDbType.Int);
                    //EventId.Value = myEvent.EventId;
                    command.Parameters.AddWithValue("id", myEvent.EventId);
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



        // Get All Events
        public List<Object> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                List<Object> events = new List<Object>();
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Select * from Event";

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Event newEvent = new Event
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
                        events.Add(newEvent);
                    }
                    return events;
                }
            }
        }


    }

}
