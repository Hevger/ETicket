using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Selectors;
using System.ServiceModel;

namespace ETicketServiceHost
{
    public class UserValidator : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            if (userName != "ETicket" || password != "ETicketPass")
            {
                throw new FaultException("Brugernavn eller password er forkert!");
            }
        }
    }
}
