using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface ICRUD
    {
        void Create(Object obj);
        Object Get(int id);
        void Delete(int id);
        void Update(Object obj);
        List<Object> GetAll();
    }
}
