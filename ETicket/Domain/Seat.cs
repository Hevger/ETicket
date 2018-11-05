using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [DataContract]
    public class Seat
    {
        [DataMember]
        public int SeatId { get; set; }

        [DataMember]
        public int SeatNumber { get; set; }

        [DataMember]
        public int EventId { get; set; }

        [DataMember]
        public bool Available { get; set; }
    }
}
