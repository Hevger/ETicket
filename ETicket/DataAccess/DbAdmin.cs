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
    public class DbAdmin : ICRUD
    {
        string connectionString = ConfigurationManager.ConnectionStrings["Kraka"].ConnectionString;

        // Create Admin
        public void Create(Object obj)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    Admin myAdmin = (Admin)obj;
                    command.CommandText = "Insert into Admin (Name, PhoneNumber, Email, Password, GUID) values (@Name, @PhoneNumber, @Email, @Password, @GUID)";
                    command.Parameters.AddWithValue("Name", myAdmin.Name);
                    command.Parameters.AddWithValue("PhoneNumber", myAdmin.PhoneNumber);
                    command.Parameters.AddWithValue("Email", myAdmin.Email);
                    command.Parameters.AddWithValue("Password", myAdmin.Password);
                    command.Parameters.AddWithValue("GUID", myAdmin.GUID);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Delete Admin
        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Delete from Admin where AdminId = @id";
                    //SqlParameter AdminId = command.Parameters.Add("@id", SqlDbType.Int);
                    //AdminId.Value = id;
                    command.Parameters.AddWithValue("id", id);

                    command.ExecuteNonQuery();
                }
            }
        }

        // Get Admin
        public object Get(int id = 0)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Admin newAdmin = null;
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Select * from Admin where AdminId = @id";
                    //SqlParameter AdminId = command.Parameters.Add("@id", SqlDbType.Int);
                    //AdminId.Value = id;
                    command.Parameters.AddWithValue("id", id);

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        newAdmin = new Admin
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("AdminId")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            Password = reader.GetString(reader.GetOrdinal("Password")),
                            GUID = reader.GetString(reader.GetOrdinal("GUID"))
                        };
                    }
                    return newAdmin;
                }
            }
        }


        // Update Admin
        public void Update(object obj)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    Admin myAdmin = (Admin)obj;
                    command.CommandText = "Update Admin SET Name = @Name, PhoneNumber = @PhoneNumber, Email = @Email, Password = @Password where AdminId = @id";
                    //SqlParameter AdminId = command.Parameters.Add("@id", SqlDbType.Int);
                    //AdminId.Value = myAdmin.Id;
                    command.Parameters.AddWithValue("id", myAdmin.Id);

                    command.Parameters.AddWithValue("Name", myAdmin.Name);
                    command.Parameters.AddWithValue("PhoneNumber", myAdmin.PhoneNumber);
                    command.Parameters.AddWithValue("Email", myAdmin.Email);
                    command.Parameters.AddWithValue("Password", myAdmin.Password);

                    command.ExecuteNonQuery();
                }
            }
        }



        // Get All Admins
        public List<Object> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                List<Object> admins = new List<Object>();
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Select AdminId, Name, PhoneNumber, Email, Password, GUID from Admin";

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Admin newAdmin = new Admin
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("AdminId")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            Password = reader.GetString(reader.GetOrdinal("Password")),
                            GUID = reader.GetString(reader.GetOrdinal("GUID"))
                        };
                        admins.Add(newAdmin);
                    }
                    return admins;
                }
            }
        }


    }
}
