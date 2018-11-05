using BusinessLogic;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicketService
{
    public class ETicketServiceClass : ICustomerService, IEventService, IAdminService, IOrderService, ITicketService, ISeatService
    {
        private CustomerController customerC = new CustomerController();
        private EventController eventC = new EventController();
        private AdminController adminC = new AdminController();
        private OrderController orderC = new OrderController();
        private TicketController ticketC = new TicketController();
        private SeatController seatC = new SeatController();


        #region Customer
        // Create Customer
        public void CreateCustomer(Customer customer)
        {
            customerC.Create(customer);
        }

        // Delete Customer
        public void DeleteCustomer(int id)
        {
            customerC.Delete(id);
        }

        // Get Customer
        public Customer GetCustomer(int id)
        {
            return (Customer)customerC.Get(id);
        }

        // Update Customer
        public void UpdateCustomer(Customer customer)
        {
            customerC.Update(customer);
        }

        // Get All Customers
        public List<Customer> GetAllCustomers()
        {
            List<Customer> customers = new List<Customer>(customerC.GetAll().Cast<Customer>());
            return customers;
        }

        #endregion

        #region Event
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
            return (Event)eventC.Get(id);
        }

        // Update Event
        public void UpdateEvent(Event myEvent)
        {
            eventC.Update(myEvent);
        }

        // Get All Events
        public List<Event> GetAllEvents()
        {
            List<Event> events = new List<Event>(customerC.GetAll().Cast<Event>());
            return events;
        }
        #endregion


        #region Admin
        // Get Admin
        public Admin GetAdmin(int id)
        {
           return (Admin) adminC.Get(id);
        }

        // Create Admin
        public void CreateAdmin(Admin myAdmin)
        {
            adminC.Create(myAdmin);
        }

        // Delete Admin
        public void DeleteAdmin(int id)
        {
            adminC.Delete(id);
        }

        // Get All Admins
        public List<Admin> GetAllAdmins()
        {
            List<Admin> admins = new List<Admin>(adminC.GetAll().Cast<Admin>());
            return admins;
        }

        // Update Admin
        public void UpdateAdmin(Admin myAdmin)
        {
            adminC.Update(myAdmin);
        }
        #endregion


        #region Order

        // Get Order
        public Order GetOrder(int id)
        {
            return (Order)orderC.Get(id);  
        }

        // Create Order
        public void CreateOrder(Order myOrder)
        {
            orderC.Create(myOrder);
        }


        // Delete Order
        public void DeleteOrder(int id)
        {
            orderC.Delete(id);
        }

        // Update Order
        public void UpdateOrder(Order myOrder)
        {
            orderC.Update(myOrder);
        }

        // Get All Orders
        public List<Order> GetAllOrders()
        {
            List<Order> orders = new List<Order>(orderC.GetAll().Cast<Order>());
            return orders;
        }
        #endregion


        #region Ticket

        // Get Ticket
        public Ticket GetTicket(int id)
        {
            return (Ticket)ticketC.Get(id);
        }

        // Create Ticket 
        public void CreateTicket(Ticket myTicket)
        {
            ticketC.Create(myTicket);
        }

        // Delete Ticket
        public void DeleteTicket(int id)
        {
            ticketC.Delete(id);
        }

        // Update Ticket
        public void UpdateTicket(Ticket myTicket)
        {
            ticketC.Update(myTicket);
        }

        // Get All Tickets
        public List<Ticket> GetAllTickets()
        {
            List<Ticket> tickets = new List<Ticket>(ticketC.GetAll().Cast<Ticket>());
            return tickets;
        }

        #endregion


        #region Seat
        // Get Seat
        public Seat GetSeat(int id)
        {
            return (Seat) seatC.Get(id);
        }

        // Delete Seat
        public void DeleteSeat(int id)
        {
            seatC.Delete(id);
        }

        // Create Seat
        public void CreateSeat(Seat mySeat)
        {
            seatC.Create(mySeat);
        }

        // Update Seat
        public void UpdateSeat(Seat mySeat)
        {
            seatC.Update(mySeat);
        }


        // Get All Seats
        public List<Seat> GetAllSeats()
        {
            List<Seat> seats = new List<Seat>(ticketC.GetAll().Cast<Seat>());
            return seats;
        }
        #endregion



    }
}
