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
    public class DbAdmin
    {
        string connectionString = ConfigurationManager.ConnectionStrings["Kraka"].ConnectionString;
        public AdminInfo getAdminInfo(string adminUsername)
        {
            AdminInfo adminInfo = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Select UserName, PasswordHash from Users INNER JOIN UserRole ON Users.Id = UserRole.UserId INNER JOIN Role ON UserRole.RoleId = Role.Id WHERE Role.Name = 'admin' AND Users.UserName = @adminUsername";
                    command.Parameters.AddWithValue("adminUsername", adminUsername);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        adminInfo = new AdminInfo
                        {
                            Username = reader.GetString(reader.GetOrdinal("UserName")),
                            Password = reader.GetString(reader.GetOrdinal("PasswordHash"))
                        };
                    }
                }
            }
            return adminInfo;
        }
    }
}
