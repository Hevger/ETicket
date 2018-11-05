using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class SeatController
    {
        private ICRUD crud = new DbSeat();
        public void Create(Object seat) => crud.Create(seat);
        public object Get(int id) => crud.Get(id);
        public void Delete(int id) => crud.Delete(id);
        public void Update(Object seat) => crud.Update(seat);
        public List<Object> GetAll() => crud.GetAll();
    }
}
