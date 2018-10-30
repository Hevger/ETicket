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
            DbCustomer dbc = new DbCustomer();

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

            Event newEvent = (Event) db.Get(8);
            Console.WriteLine(newEvent.ToString());

            Console.WriteLine("-----");

            myEvent = new Event()
            {
                Title = "Test",
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


            Guid g;
            g = Guid.NewGuid();
            string ng = Convert.ToString(g);

            Customer cus = new Customer()
            {
                Name = "WhatEver",
                PhoneNumber = "004560606060",
                Email = "DetErIkkeMig@hotmail.com",
                Password = "123456",
                GUID = ng
            };

            //dbc.Create(cus);
            //dbc.Delete(2);
            Console.WriteLine(dbc.Get(1));

            cus = new Customer()
            {
                Name = "Hevger",
                PhoneNumber = "004560606060",
                Email = "DetErIkkeMig@hotmail.com",
                Password = "123456",
                GUID = ng
            };

            cus.Id = 1;
            dbc.Update(cus);
        }
    }
}
