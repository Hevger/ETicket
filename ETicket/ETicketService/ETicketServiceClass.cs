using BusinessLogic;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicketService
{
    public class ETicketServiceClass : ICustomerService, IEventService
    {
        private CustomerController customerC = new CustomerController();
        private EventController eventC = new EventController();

        // Create Customer
        public void CreateCustomer(Customer customer)
        {

        }

        // Delete Customer
        public void DeleteCustomer(int id)
        {
        }

        // Get Customer
        public Customer GetCustomer(int id)
        {
            return (Customer)customerC.Get(id);
        }

        // Update Customer
        public void UpdateCustomer(Customer customer)
        {
        }

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
            return (Event)eventC.Get(id);
        }

        // Update Event
        public void UpdateEvent(Event myEvent)
        {
            eventC.Update(myEvent);
        }
    }
}
