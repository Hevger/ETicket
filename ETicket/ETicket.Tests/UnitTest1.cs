using System;
using DataAccess;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ETicket.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private Event myEvent;
        private DbEvent db;
        private int IdOfMyEvent;

        [TestInitialize]
        public void Setup()
        {
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

            db = new DbEvent();
        }

        [TestMethod]
        public void CreateEvent_WhenCalled()
        {             
            IdOfMyEvent = db.Create(myEvent);
            Assert.AreEqual(myEvent, db.Get(IdOfMyEvent));
        }

        [TestCleanup]
        public void CleanUp()
        {
            db.Delete(IdOfMyEvent);
        }

    }
}
