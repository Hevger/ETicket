using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class EventController 
    {
        private ICRUD crud = new DbEvent();

        public void Create(Object myEvent) => crud.Create(myEvent);
        public object Get(int id) => crud.Get(id);
        public void Delete(int id) => crud.Delete(id);
        public void Update(Object myEvent) => crud.Update(myEvent);
        public List<Object> GetAll() => crud.GetAll();

    }
}
