using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class AdminController
    {
        private DbAdmin dbAdmin = new DbAdmin();
        public AdminInfo GetAdminInfo(string adminUsername) => dbAdmin.getAdminInfo(adminUsername);
    }
}
