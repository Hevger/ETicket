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
    public class DbCustomer : ICRUD
    {
        string connectionString = ConfigurationManager.ConnectionStrings["Kraka"].ConnectionString;

        // Create Customer
        public void Create(Object obj)
        {
            using (SqlConnection connection = new SqlConnection(connectionString)) 
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    Customer myCustomer = (Customer)obj;
                    command.CommandText = "Insert into Customer (Name, PhoneNumber, Email, Password, GUID) values (@Name, @PhoneNumber, @Email, @Password, @GUID)";
                    command.Parameters.AddWithValue("Name", myCustomer.Name);
                    command.Parameters.AddWithValue("PhoneNumber", myCustomer.PhoneNumber);
                    command.Parameters.AddWithValue("Email", myCustomer.Email);
                    command.Parameters.AddWithValue("Password", myCustomer.Password);
                    command.Parameters.AddWithValue("GUID", myCustomer.GUID);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Delete Customer
        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Delete from Customer where CustomerId = @id";
                    //SqlParameter CustomerId = command.Parameters.Add("@id", SqlDbType.Int);
                    //CustomerId.Value = id;
                    command.Parameters.AddWithValue("id", id);

                    command.ExecuteNonQuery();
                }
            }
        }

        // Get Customer
        public object Get(int id = 0)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Customer newCustomer = null;
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Select * from Customer where CustomerId = @id";
                    //SqlParameter CustomerId = command.Parameters.Add("@id", SqlDbType.Int);
                    //CustomerId.Value = id;
                    command.Parameters.AddWithValue("id", id);

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        newCustomer = new Customer
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("CustomerId")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            Password = reader.GetString(reader.GetOrdinal("Password")),
                            GUID = reader.GetString(reader.GetOrdinal("GUID"))
                        };
                    }
                    return newCustomer;
                }
            }
        }


        // Get All Customers
        public List<Object> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                List<Object> customers = new List<Object>();
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Select CustomerId, Name, PhoneNumber, Email, Password, GUID from Customer";
             
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Customer newCustomer = new Customer
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("CustomerId")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            Password = reader.GetString(reader.GetOrdinal("Password")),
                            GUID = reader.GetString(reader.GetOrdinal("GUID"))
                        };
                        customers.Add(newCustomer);
                    }
                    return customers;
                }
            }
        }

        // Update Customer
        public void Update(object obj)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    Customer myCustomer = (Customer)obj;
                    command.CommandText = "Update Customer SET Name = @Name, PhoneNumber = @PhoneNumber, Email = @Email, Password = @Password where CustomerId = @id";
                    //SqlParameter CustomerId = command.Parameters.Add("@id", SqlDbType.Int);
                    //CustomerId.Value = myCustomer.Id;
                    command.Parameters.AddWithValue("id", myCustomer.Id);

                    command.Parameters.AddWithValue("Name", myCustomer.Name);
                    command.Parameters.AddWithValue("PhoneNumber", myCustomer.PhoneNumber);
                    command.Parameters.AddWithValue("Email", myCustomer.Email);
                    command.Parameters.AddWithValue("Password", myCustomer.Password);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
