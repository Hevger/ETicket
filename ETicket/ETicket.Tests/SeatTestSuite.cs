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
    public class SeatTestSuite
    {
        private Event myEvent;
        private Seat mySeat;
        private DbEvent dbEvent;
        private DbSeat dbSeat;
        private int IdOfMyEvent;
        private int IdOfSeat;
        string connectionString = ConfigurationManager.ConnectionStrings["Kraka"].ConnectionString;


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

            mySeat = new Seat
            {
                Available = true
            };

            dbEvent = new DbEvent();
            dbSeat = new DbSeat();
        }

        [TestMethod]
        public void CreateSeat_WhenCalled()
        {
            IdOfMyEvent = dbEvent.Create(myEvent);
            mySeat.EventId = IdOfMyEvent;
            Event currentEvent = (Event) dbEvent.Get(IdOfMyEvent);
            mySeat.SeatNumber = currentEvent.AvailableTickets;
            IdOfSeat = dbSeat.Create(mySeat);
        }

        [TestMethod]
        public void DeleteSeat_WhenCalled()
        {
            IdOfMyEvent = dbEvent.Create(myEvent);
            mySeat.EventId = IdOfMyEvent;
            Event currentEvent = (Event)dbEvent.Get(IdOfMyEvent);
            mySeat.SeatNumber = currentEvent.AvailableTickets;
            IdOfSeat = dbSeat.Create(mySeat);
            dbSeat.Delete(IdOfSeat);
            dbEvent.Delete(IdOfMyEvent);
            Assert.IsNull(dbSeat.Get(IdOfSeat));
        }

        [TestMethod]
        public void UpdateSeat_WhenCalled()
        {
            IdOfMyEvent = dbEvent.Create(myEvent);
            mySeat.EventId = IdOfMyEvent;
            Event currentEvent = (Event)dbEvent.Get(IdOfMyEvent);
            mySeat.SeatNumber = currentEvent.AvailableTickets;
            IdOfSeat = dbSeat.Create(mySeat);
            Seat currentSaet = (Seat) dbSeat.Get(IdOfSeat);
            currentSaet.Available = false;
            dbSeat.Update(currentSaet);
            Assert.IsFalse(((Seat)dbSeat.Get(IdOfSeat)).Available);
        }

        [TestCleanup]
        public void CleanUp()
        {
            dbSeat.Delete(IdOfSeat);
            dbEvent.Delete(IdOfMyEvent);
            myEvent = null;
            mySeat = null;
            dbSeat = null;
            dbEvent = null;
            IdOfMyEvent = 0;
            IdOfSeat = 0;
        }
    }
}
