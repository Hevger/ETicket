using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class TicketController
    {
        private ICRUD crud = new DbTicket();
        public void Create(Object ticket) => crud.Create(ticket);
        public object Get(int id) => crud.Get(id);
        public void Delete(int id) => crud.Delete(id);
        public void Update(Object ticket) => crud.Update(ticket);
        public List<Object> GetAll() => crud.GetAll();
    }
}
