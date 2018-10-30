using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            DbEvent db = new DbEvent();
            Event myEvent = new Event()
            {
                Title = "Test",
                Description = "Kharai 3elikm",
                Gate = "8b8",
                GateOpens = DateTime.Now,
                StartTime = DateTime.Now,
                Date = DateTime.Now,
                AvailableTickets = 30,
                TicketPrice = 35.50M
            };
       
            db.Create(myEvent);

            Console.WriteLine("-----");

            Event newEvent = (Event) db.Get(1);
            Console.WriteLine(newEvent.ToString());

            Console.WriteLine("-----");

            myEvent = new Event()
            {
                Title = "Changed",
                Description = "Changed Changed",
                Gate = "8b8",
                GateOpens = DateTime.Now,
                StartTime = DateTime.Now,
                Date = DateTime.Now,
                AvailableTickets = 30,
                TicketPrice = 35.50M
            };

            myEvent.EventId = 6;

            db.Update(myEvent);
            
        }
    }
}
