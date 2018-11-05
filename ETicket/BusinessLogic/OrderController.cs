using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class OrderController
    {
        private ICRUD crud = new DbOrder();
        public void Create(Object order) => crud.Create(order);
        public object Get(int id) => crud.Get(id);
        public void Delete(int id) => crud.Delete(id);
        public void Update(Object order) => crud.Update(order);
        public List<Object> GetAll() => crud.GetAll();
    }
}
