using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic;
using Domain;

namespace ETicketService
{
    class CustomerService : ICustomerService
    {
        private CustomerController customerC = new CustomerController();

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
            return (Customer) customerC.Get(id);
        }

        // Update Customer
        public void UpdateCustomer(Customer customer)
        {
        }
    }
}
