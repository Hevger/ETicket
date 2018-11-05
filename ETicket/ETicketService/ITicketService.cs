using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ETicketService
{
    [ServiceContract]
    public interface ITicketService
    {
        [OperationContract]
        Ticket GetTicket(int id);

        [OperationContract]
        void CreateTicket(Ticket myTicket);

        [OperationContract]
        void DeleteTicket(int id);

        [OperationContract]
        void UpdateTicket(Ticket myTicket);

        [OperationContract]
        List<Ticket> GetAllTickets();
    }
}
