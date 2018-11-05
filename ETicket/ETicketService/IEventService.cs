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
    public interface IEventService
    {
        [OperationContract]
        Event GetEvent(int id);

        [OperationContract]
        void CreateEvent(Event myEvent);

        [OperationContract]
        void DeleteEvent(int id);

        [OperationContract]
        void UpdateEvent(Event myEvent);

        [OperationContract]
        List<Event> GetAllEvents();

    }
}
