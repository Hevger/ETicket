using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DbEvent
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ETicketDb"].ConnectionString;


        public void CreateEvent(Event event)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command
            }

        }
        

    }

}
