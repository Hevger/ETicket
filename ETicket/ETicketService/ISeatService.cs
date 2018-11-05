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
    public interface ISeatService
    {
        [OperationContract]
        Seat GetSeat(int id);

        [OperationContract]
        void DeleteSeat(int id);

        [OperationContract]
        void CreateSeat(Seat mySeat);

        [OperationContract]
        void UpdateSeat(Seat mySeat);

        [OperationContract]
        List<Seat> GetAllSeats();
    }
}
