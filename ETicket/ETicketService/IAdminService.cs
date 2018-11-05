using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ETicketService
{
    [ServiceContract]
    public interface IAdminService
    {
        [OperationContract]
        Admin GetAdmin(int id);

        [OperationContract]
        void CreateAdmin(Admin myAdmin);

        [OperationContract]
        void DeleteAdmin(int id);

        [OperationContract]
        void UpdateAdmin(Admin myAdmin);

        [OperationContract]
        List<Admin> GetAllAdmins();
    }
}
