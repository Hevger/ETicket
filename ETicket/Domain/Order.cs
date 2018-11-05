using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [DataContract]
    public class Order
    {
        [DataMember]
        public int OrderId { get; set; }

        [DataMember]
        public decimal TotalPrice { get; set; }

        [DataMember]
        public DateTime Date { get; set; }

        [DataMember]
        public int Quantity { get; set; }

        [DataMember]
        public int CustomerId { get; set; }

        [DataMember]
        public List<Ticket> Tickets { get; set; }
    }
}
