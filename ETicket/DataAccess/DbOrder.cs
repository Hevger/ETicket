using Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DataAccess
{
    public class DbOrder : ICRUD
    {

        public DbOrder()
        {

        }
        string connectionString = ConfigurationManager.ConnectionStrings["Kraka"].ConnectionString;

        // Create Order
        public int Create(object obj)
        {
            int insertedOrderId;
            TransactionOptions options = new TransactionOptions();
            options.IsolationLevel = IsolationLevel.Serializable;
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
                {

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        // SqlTransaction transaction = connection.BeginTransaction();
                        using (SqlCommand command = new SqlCommand("", connection))
                        {
#region method logic

                            Order myOrder = (Order)obj;
                            Event myEvent = new Event();
                            Event currentEvent = new Event();
                            DbEvent dbEvent = new DbEvent();
                            currentEvent = (Event)dbEvent.Get(myOrder.EventId, connection);
                            int availableTicketsOfCurrentEvent = currentEvent.AvailableTickets;
                            command.CommandText = "Insert into Orders (TotalPrice, Date, Quantity, CustomerId, EventId) values (@TotalPrice, @Date, @Quantity, @CustomerId, @EventId); SELECT SCOPE_IDENTITY()";
                            command.Parameters.AddWithValue("TotalPrice", myOrder.TotalPrice);
                            command.Parameters.AddWithValue("Date", myOrder.Date);
                            command.Parameters.AddWithValue("Quantity", myOrder.Quantity);
                            command.Parameters.AddWithValue("CustomerId", myOrder.CustomerId);
                            command.Parameters.AddWithValue("EventId", myOrder.EventId);
                            insertedOrderId = Convert.ToInt32(command.ExecuteScalar());
#endregion

                            int x = myOrder.Quantity;

                            while (x > 0)
                            {
                                #region create seats and tickets
                                SqlCommand command1 = new SqlCommand("", connection);
                                Seat newSeat = new Seat();
                                myEvent = (Event)dbEvent.Get(myOrder.EventId, connection);
                                int availableTickets = myEvent.AvailableTickets;

                                newSeat.SeatNumber = availableTickets;
                                newSeat.Available = true;
                                newSeat.EventId = myOrder.EventId;
                                DbSeat dbSeat = new DbSeat();
                                int inseretedSeatId = dbSeat.Create(newSeat, connection);

                                Ticket newTicket = new Ticket();
                                newTicket.EventId = myOrder.EventId;
                                newTicket.SeatId = inseretedSeatId;
                                newTicket.CustomerId = myOrder.CustomerId;
                                DbTicket dbTicket = new DbTicket();
                                int TicketId = dbTicket.Create(newTicket, connection);
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
                                #endregion
                            }

                            if (myOrder.Quantity <= availableTicketsOfCurrentEvent && myOrder.Quantity > 0)
                            {
                                scope.Complete();
                                connection.Close();
                            }
                            else
                            {
                                scope.Dispose();
                            }


                        }

                        return insertedOrderId;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return 0;
            }
        }

        public void Cancel(Order order)
        {
            DbEvent dbEvent = new DbEvent();
            Order currentOrder = (Order)Get(order.OrderId);
            int Quantity = currentOrder.Quantity;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    SqlCommand deleteTicket = connection.CreateCommand();
                    SqlCommand deleteTicketAndSeat = connection.CreateCommand();
                    SqlCommand updateEventTickets = connection.CreateCommand();
                    command.CommandText = "Delete from OrderItems where OrderId = @id; Delete from Orders where OrderId = @id;";
                    command.Parameters.AddWithValue("id", order.OrderId);

                    List<Ticket> tickets = GetOrderTickets(order.OrderId);

                    command.ExecuteNonQuery();

                    foreach (var item in tickets)
                    {
                        deleteTicketAndSeat.CommandText = "Delete from Ticket where TicketId = @TicketId; Delete from Seat where SeatId = @SeatId";
                        deleteTicketAndSeat.Parameters.AddWithValue("TicketId", item.TicketId);
                        deleteTicketAndSeat.Parameters.AddWithValue("SeatId", item.SeatId);
                        deleteTicketAndSeat.ExecuteNonQuery();
                        deleteTicketAndSeat.Parameters.Clear();
                    }

                    Event orderEvent = (Event)dbEvent.Get(order.EventId);
                    int AvailableTickets = orderEvent.AvailableTickets;
                    updateEventTickets.CommandText = "Update Event set AvailableTickets = @newAvailableTickets where EventId = @EventId";
                    updateEventTickets.Parameters.AddWithValue("newAvailableTickets", Quantity + AvailableTickets);
                    updateEventTickets.Parameters.AddWithValue("EventId", order.EventId);
                    updateEventTickets.ExecuteNonQuery();

                }
            }
        }


        public List<Order> GetCustomerOrdersByUsername(string Username)
        {
            List<Order> orders = new List<Order>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {   
                    command.CommandText = "SELECT * FROM Orders INNER JOIN Users ON Users.Id = Orders.CustomerId WHERE Users.Username = @Username";
                    command.Parameters.AddWithValue("Username", Username);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Order myOrder = new Order();
                        myOrder.OrderId = reader.GetInt32(reader.GetOrdinal("OrderId"));
                        myOrder.EventId = reader.GetInt32(reader.GetOrdinal("EventId"));
                        myOrder.TotalPrice = reader.GetDecimal(reader.GetOrdinal("TotalPrice"));
                        myOrder.Date = reader.GetDateTime(reader.GetOrdinal("Date"));
                        myOrder.Quantity = reader.GetInt32(reader.GetOrdinal("Quantity"));
                        myOrder.CustomerId = reader.GetString(reader.GetOrdinal("CustomerId"));
                        orders.Add(myOrder);
                    }

                }
                return orders;
            }
        }


        public List<Ticket> GetOrderTickets(int id)
        {
            List<Ticket> tickets = new List<Ticket>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Ticket FULL OUTER JOIN OrderItems ON OrderItems.TicketId = Ticket.TicketId WHERE OrderId = @OrderId";
                    command.Parameters.AddWithValue("OrderId", id);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Ticket myTicket = new Ticket();
                        myTicket.TicketId = reader.GetInt32(reader.GetOrdinal("TicketId"));
                        myTicket.SeatId = reader.GetInt32(reader.GetOrdinal("SeatId"));
                        myTicket.EventId = reader.GetInt32(reader.GetOrdinal("EventId"));
                        myTicket.CustomerId = reader.GetString(reader.GetOrdinal("CustomerId"));
                        tickets.Add(myTicket);
                    }

                }
            }
            return tickets;
        }

        // Delete Order
        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Delete from Orders where OrderId = @id";
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
                    command.CommandText = "Select * from Orders where OrderId = @id";
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
                            CustomerId = reader.GetString(reader.GetOrdinal("CustomerId")),
                            EventId = reader.GetInt32(reader.GetOrdinal("EventId"))
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
                    command.CommandText = "Select * from Orders";

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

        public string GetRandomUser()
        {
            string UserId = string.Empty;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Select TOP 1 Users.Id from Users INNER JOIN UserRole ON Users.Id = UserRole.UserId INNER JOIN Role ON UserRole.RoleId = Role.Id WHERE Role.Name = 'customer' ";

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        UserId = reader.GetString(reader.GetOrdinal("Id"));
                    }
                    return UserId;
                }
            }
        }





        // Get All Order for Customer
        public List<Order> GetOrdersOfCustomer(string CustomerId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                List<Order> orders = new List<Order>();
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Select * from Orders WHERE CustomerId = @CustomerId";
                    command.Parameters.AddWithValue("CustomerId", CustomerId);

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Order newOrder = new Order
                        {
                            OrderId = reader.GetInt32(reader.GetOrdinal("OrderId")),
                            TotalPrice = reader.GetDecimal(reader.GetOrdinal("TotalPrice")),
                            Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                            Quantity = reader.GetInt32(reader.GetOrdinal("Quantity")),
                            CustomerId = reader.GetString(reader.GetOrdinal("CustomerId")),
                            EventId = reader.GetInt32(reader.GetOrdinal("EventId")),

                        };
                        orders.Add(newOrder);
                    }
                    return orders;
                }
            }
        }



    }
}
