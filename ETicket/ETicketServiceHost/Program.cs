using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ETicketServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(ETicketService.ETicketServiceClass)))
            {
                host.Open();
                Console.WriteLine("Hosting started at @ " + DateTime.Now);
                Console.ReadLine();
            }
        }
    }
}
