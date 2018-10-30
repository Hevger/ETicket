using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    interface ICRUD
    {
        void Create(Object obj);
        Object Get(int id);
        void Delete(int id);
        void Update(int id, Object obj);

    }
}
