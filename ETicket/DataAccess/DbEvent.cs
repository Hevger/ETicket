using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DbEvent
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ETicketDb"].ConnectionString;


        public void CreateEvent()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = $"Insert into Event values ('DanceNotDance','Dance or dont', 'F0',  {DateTime.Now},  {DateTime.Now},  {DateTime.Now}, 30, 35.00)";
                    command.ExecuteNonQuery();
                }
            }
        }
        

    }

}
