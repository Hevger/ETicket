using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [DataContract]
    public class Ticket
    {
        [DataMember]
        public int TicketId { get; set; }

        [DataMember]
        public int SeatId { get; set; }

        [DataMember]
        public int EventId { get; set; }

        [DataMember]
        public int CustomerId { get; set; }
    }
}
