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
    public interface ICustomerService
    {
        [OperationContract]
        Customer GetCustomer(int id);

        [OperationContract]
        void CreateCustomer(Customer customer);

        [OperationContract]
        void DeleteCustomer(int id);

        [OperationContract]
        void UpdateCustomer(Customer customer);

        [OperationContract]
        List<Customer> GetAllCustomers();

    }
}
