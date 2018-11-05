using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class AdminController
    {
        private ICRUD crud = new DbAdmin();
        public void Create(Object admin) => crud.Create(admin);
        public object Get(int id) => crud.Get(id);
        public void Delete(int id) => crud.Delete(id);
        public void Update(Object admin) => crud.Update(admin);
        public List<Object> GetAll() => crud.GetAll();
    }
}
