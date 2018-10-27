using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Seat
    {
        public int SeatId { get; set; }
        public int SeatNumber { get; set; }
        public int EventId { get; set; }
        public int Available { get; set; }
    }
}
