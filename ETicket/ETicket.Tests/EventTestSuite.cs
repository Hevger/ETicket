using System;
using System.Configuration;
using System.Data.SqlClient;
using DataAccess;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ETicket.Tests
{
    [TestClass]
    public class EventTestSuite
    {
        private Event myEvent;
        private DbEvent db;
        private int IdOfMyEvent;
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

            db = new DbEvent();
        }


        [TestMethod]
        public void CreateEvent_WhenCalled()
        {             
            IdOfMyEvent = db.Create(myEvent);
            Assert.AreEqual(myEvent, db.Get(IdOfMyEvent));
        }

        [TestMethod]
        public void UpdateEvent_WhenCalled()
        {
            IdOfMyEvent = db.Create(myEvent);
            myEvent.EventId = IdOfMyEvent;
            myEvent.Title = "Hevgers Title is The Master";
            db.Update(myEvent);
            Assert.AreEqual(myEvent, db.Get(IdOfMyEvent));
        }


        [TestMethod]
        public void DeleteEvent_WhenCalled()
        {
            IdOfMyEvent = db.Create(myEvent);
            db.Delete(IdOfMyEvent);
            Assert.IsNull(db.Get(IdOfMyEvent));
        }



        [TestCleanup]
        public void CleanUp()
        {
            db.Delete(IdOfMyEvent);
            using (SqlConnection connection = new SqlConnection(connectionString)){

                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "DBCC CHECKIDENT('Event', RESEED, @IdOfMyEvent)";
                    command.Parameters.AddWithValue("IdOfMyEvent", IdOfMyEvent-1);
                    command.ExecuteNonQuery();
                }
            }

            myEvent = null;
            db = null;
            IdOfMyEvent = 0;
        }

    }
}
