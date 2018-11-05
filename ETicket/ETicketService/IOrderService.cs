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
    public interface IOrderService
    {
        [OperationContract]
        Order GetOrder(int id);

        [OperationContract]
        void CreateOrder(Order myOrder);

        [OperationContract]
        void DeleteOrder(int id);

        [OperationContract]
        void UpdateOrder(Order myOrder);

        [OperationContract]
        List<Order> GetAllOrders();
    }
}
