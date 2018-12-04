using BusinessLogic;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicketService
{
    public class ETicketServiceClass : IEventService, IOrderService, ITicketService, ISeatService, IAdminService
    {
        private EventController eventC = new EventController();
        private OrderController orderC = new OrderController();
        private TicketController ticketC = new TicketController();
        private SeatController seatC = new SeatController();
        private AdminController adminC = new AdminController();



        #region Admin
        public AdminInfo GetAdminInfo(string AdminUsername)
        {
            return adminC.GetAdminInfo(AdminUsername);
        }
        #endregion

        #region Event
        // Create Event
        public int CreateEvent(Event myEvent)
        {
            return eventC.Create(myEvent);
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
            List<Event> events = new List<Event>(eventC.GetAll().Cast<Event>());
            return events;
        }
        #endregion


        

        #region Order

        // Get Order
        public Order GetOrder(int id)
        {
            return (Order)orderC.Get(id);  
        }

        // Create Order
        public int CreateOrder(Order myOrder)
        {
           return orderC.Create(myOrder);
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



        // Get Customer Orders By Username
        public List<Order> GetCustomerOrdersByUsername(string Username)
        {
            List<Order> orders = orderC.GetCustomerOrdersByUsername(Username);
            return orders;
        }

        // Get All Orders of Customer
        public List<Order> GetOrdersOfCustomer(string CustomerId)
        {
            List<Order> orders = new List<Order>(orderC.GetOrdersOfCustomer(CustomerId).Cast<Order>());
            return orders;
        }


        // Get All Tickets Of Order
        public List<Ticket> GetOrderTickets(int id)
        {
            List<Ticket> tickes = new List<Ticket>(orderC.GetOrderTickets(id));
            return tickes;
        }

        // Cancel order
        public void Cancel(Order order)
        {
            orderC.Cancel(order);
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
