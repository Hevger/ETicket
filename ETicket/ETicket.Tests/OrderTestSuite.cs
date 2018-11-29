using DataAccess;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicket.Tests
{
    [TestClass]
    public class OrderTestSuite
    {
        private Event myEvent;
        private Order myOrder;
        private DbEvent eventDb;
        private DbOrder orderDb;
        private int IdOfMyEvent;
        private int insertedOrderId;
        string connectionString = ConfigurationManager.ConnectionStrings["Kraka"].ConnectionString;

        [TestInitialize]
        public void Setup() {
            myEvent = new Event
            {
                Title = "This is a Test Event",
                Description = "Thsi is the description of the test event",
                Gate = "FB22",
                GateOpens = new DateTime(2018, 11, 20, 12, 00, 00),
                StartTime = new DateTime(2018, 11, 20, 12, 15, 00),
                Date = DateTime.Now.Date,
                AvailableTickets = 20,
                TicketPrice = 10
            };

            

            eventDb = new DbEvent();
            orderDb = new DbOrder();
            string customerId = orderDb.GetRandomUser();

            myOrder = new Order
            {
                Quantity = 3,
                Date = DateTime.Now.Date,
                CustomerId = customerId
            };

            myOrder.TotalPrice = myOrder.Quantity * myEvent.TicketPrice;
        }

        [TestMethod]
        public void CreateOrder_WhenCalled()
        {
            IdOfMyEvent = eventDb.Create(myEvent);
            myOrder.EventId = IdOfMyEvent;
            insertedOrderId = orderDb.Create(myOrder);
            Order getMyOrder = (Order) orderDb.Get(insertedOrderId);
            Assert.AreEqual(myOrder, getMyOrder);
        }

        [TestCleanup]
        public void CleanUp()
        {
            Order getMyOrder = (Order)orderDb.Get(insertedOrderId);
            orderDb.Cancel(getMyOrder);
            eventDb.Delete(IdOfMyEvent);
            myEvent = null;
            myOrder = null;
            insertedOrderId = 0;
            IdOfMyEvent = 0;
        }


    }
}
