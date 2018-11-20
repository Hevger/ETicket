using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [DataContract]
    public class Event
    {
        [DataMember]
        public int EventId { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Gate { get; set; }

        [DataMember]
        public DateTime GateOpens { get; set; }

        [DataMember]
        public DateTime StartTime { get; set; }

        [DataMember]
        public DateTime Date { get; set; }

        [DataMember]
        public int AvailableTickets { get; set; }

        [DataMember]
        public decimal TicketPrice { get; set; }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Event e = (Event)obj;
                return (Title == e.Title) && 
                    (Description == e.Description)
                    &&
                    (Gate == e.Gate)
                    &&
                    (GateOpens == e.GateOpens)
                    &&
                    (StartTime == e.StartTime)
                    &&
                    (Date == e.Date)
                    &&
                    (AvailableTickets == e.AvailableTickets)
                    &&
                    (TicketPrice == e.TicketPrice);
            }
       }
    }
}
