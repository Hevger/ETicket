using System;
using DataAccess;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ETicket.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CreateEvent_WhenCalled()
        {
            Event myEvent = new Event
            {
                Title = "This is a Test Event",
                Description = "Thsi is the description of the test event",
                Gate = "FB22",
                GateOpens = DateTime.Now,
                StartTime = DateTime.Now,
                Date = DateTime.Now.Date,
                AvailableTickets = 20,
                TicketPrice = 10
            };

            DbEvent db = new DbEvent();

            int IdOfMyEvent = db.Create(myEvent);

            Assert.AreEqual(((Event)db.Get(IdOfMyEvent)).Title, myEvent.Title);
            Assert.AreEqual(((Event)db.Get(IdOfMyEvent)).Description, myEvent.Description);
            Assert.AreEqual(((Event)db.Get(IdOfMyEvent)).Gate, myEvent.Gate);
            //Assert.AreEqual(((Event)db.Get(IdOfMyEvent)).GateOpens, myEvent.GateOpens);
            //Assert.AreEqual(((Event)db.Get(IdOfMyEvent)).StartTime, myEvent.StartTime);
            Assert.AreEqual(((Event)db.Get(IdOfMyEvent)).Date, myEvent.Date);
            Assert.AreEqual(((Event)db.Get(IdOfMyEvent)).AvailableTickets, myEvent.AvailableTickets);
            Assert.AreEqual(((Event)db.Get(IdOfMyEvent)).TicketPrice, myEvent.TicketPrice);
        }
    }
}
