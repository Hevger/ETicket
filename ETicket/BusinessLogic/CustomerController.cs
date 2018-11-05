using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class CustomerController
    {
        private ICRUD crud = new DbCustomer();
        public void Create(Object customer) => crud.Create(customer);
        public object Get(int id) => crud.Get(id);
        public void Delete(int id) => crud.Delete(id);
        public void Update(Object customer) => crud.Update(customer);
        public List<Object> GetAll() => crud.GetAll();
    }
}
