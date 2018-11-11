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
    public class DbOrder : ICRUD
    {
        string connectionString = ConfigurationManager.ConnectionStrings["Kraka"].ConnectionString;
        DbSeat dbSeat = new DbSeat();
        DbTicket dbTicket = new DbTicket();
        DbEvent dbEvent = new DbEvent();

        // Create Order
        public int Create(object obj)
        {
            int insertedOrderId;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    Order myOrder = (Order)obj;
                    command.CommandText = "Insert into Orders (TotalPrice, Date, Quantity, CustomerId, EventId) values (@TotalPrice, @Date, @Quantity, @CustomerId, @EventId); SELECT SCOPE_IDENTITY()";
                    command.Parameters.AddWithValue("TotalPrice", myOrder.TotalPrice);
                    command.Parameters.AddWithValue("Date", myOrder.Date);
                    command.Parameters.AddWithValue("Quantity", myOrder.Quantity);
                    command.Parameters.AddWithValue("CustomerId", myOrder.CustomerId);
                    command.Parameters.AddWithValue("EventId", myOrder.EventId);
                    insertedOrderId = Convert.ToInt32(command.ExecuteScalar());


                    int x = myOrder.Quantity;
                    while (x > 0)
                    {
                        SqlCommand command1 = connection.CreateCommand();
                        Seat newSeat = new Seat();
                        Event myEvent = (Event) dbEvent.Get(myOrder.EventId);
                        int availableTickets = myEvent.AvailableTickets;

                        newSeat.SeatNumber = availableTickets;
                        newSeat.Available = true;
                        newSeat.EventId = myOrder.EventId;
                        int inseretedSeatId = dbSeat.Create(newSeat);

                        Ticket newTicket = new Ticket();
                        newTicket.EventId = myOrder.EventId;
                        newTicket.SeatId = inseretedSeatId;
                        newTicket.CustomerId = null;
                        int TicketId = dbTicket.Create(newTicket);
                        int EventId2 = myOrder.EventId;
                        x--;
                        availableTickets--;
                        // Minus 1 from available tickets on 
                        command1.CommandText = "Insert into OrderItems (OrderId, TicketId) values (@OrderId, @TicketId); UPDATE Event set AvailableTickets = @availableTickets WHERE EventId = @EventId2";
                        command1.Parameters.AddWithValue("OrderId", insertedOrderId);
                        command1.Parameters.AddWithValue("TicketId", TicketId);
                        command1.Parameters.AddWithValue("availableTickets", availableTickets);
                        command1.Parameters.AddWithValue("EventId2", EventId2);
                        command1.ExecuteNonQuery();
                        command1.Parameters.Clear();
                    }

                }
            }
            return insertedOrderId;
        }

        // Delete Order
        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Delete from Order where OrderId = @id";
                    command.Parameters.AddWithValue("id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Get Order
        public object Get(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Order newOrder = null;
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Select * from Order where OrderId = @id";
                    command.Parameters.AddWithValue("id", id);

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        newOrder = new Order
                        {
                            OrderId = reader.GetInt32(reader.GetOrdinal("OrderId")),
                            TotalPrice = reader.GetDecimal(reader.GetOrdinal("TotalPrice")),
                            Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                            Quantity = reader.GetInt32(reader.GetOrdinal("Quantity")),
                            CustomerId = reader.GetString(reader.GetOrdinal("CustomerId"))
                        };
                    }
                    return newOrder;
                }
            }
        }

        // Update Order
        public void Update(object obj)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    Order myOrder = (Order)obj;
                    command.CommandText = "Update Order SET TotalPrice = @TotalPrice, Date = @Date, Quantity = @Quantity, CustomerId = @CustomerId where OrderId = @OrderId";
                    command.Parameters.AddWithValue("OrderId", myOrder.OrderId);
                    command.Parameters.AddWithValue("TotalPrice", myOrder.TotalPrice);
                    command.Parameters.AddWithValue("Date", myOrder.Date);
                    command.Parameters.AddWithValue("Quantity", myOrder.Quantity);
                    command.Parameters.AddWithValue("CustomerId", myOrder.CustomerId);
                    command.ExecuteNonQuery();
                }
            }
        }



        // Get All Order
        public List<Object> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                List<Object> orders = new List<Object>();
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Select * from Order";

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Order newOrder = new Order
                        {
                            OrderId = reader.GetInt32(reader.GetOrdinal("OrderId")),
                            TotalPrice = reader.GetDecimal(reader.GetOrdinal("TotalPrice")),
                            Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                            Quantity = reader.GetInt32(reader.GetOrdinal("Quantity")),
                            CustomerId = reader.GetString(reader.GetOrdinal("CustomerId"))
                        };
                        orders.Add(newOrder);
                    }
                    return orders;
                }
            }
        }


    }
}
