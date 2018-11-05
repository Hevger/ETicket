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

        // Create Order
        public void Create(object obj)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    Order myOrder = (Order)obj;
                    command.CommandText = "Insert into Order (TotalPrice, Date, Quantity, CustomerId) values (@TotalPrice, @Date, @Quantity, @CustomerId)";
                    command.Parameters.AddWithValue("TotalPrice", myOrder.TotalPrice);
                    command.Parameters.AddWithValue("Date", myOrder.Date);
                    command.Parameters.AddWithValue("Quantity", myOrder.Quantity);
                    command.Parameters.AddWithValue("CustomerId", myOrder.CustomerId);
                    command.ExecuteNonQuery();
                }
            }
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
                            CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerId"))
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
                            CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerId"))
                        };
                        orders.Add(newOrder);
                    }
                    return orders;
                }
            }
        }


    }
}
