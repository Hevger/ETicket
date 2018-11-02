using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic;
using Domain;

namespace ETicketService
{
    public class EventService : IEventService
    {
        private EventController eventC = new EventController();

        // Create Event
        public void CreateEvent(Event myEvent)
        {
            eventC.Create(myEvent);
        }

        // Delete Event
        public void DeleteEvent(int id)
        {
            eventC.Delete(id);
        }


        // Get Event 
        public Event GetEvent(int id)
        {
            return (Event) eventC.Get(id);
        }

        // Update Event
        public void UpdateEvent(Event myEvent)
        {
            eventC.Update(myEvent);
        }
    }
}
