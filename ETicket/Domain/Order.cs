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
        public int EventId { get; set; }


        [DataMember]
        public decimal TotalPrice { get; set; }

        [DataMember]
        public DateTime Date { get; set; }

        [DataMember]
        public int Quantity { get; set; }

        [DataMember]
        public string CustomerId { get; set; }


        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Order e = (Order)obj;
                return (EventId == e.EventId) &&
                    (TotalPrice == e.TotalPrice)
                    &&
                    (Date == e.Date)
                    &&
                    (Quantity == e.Quantity)
                    &&
                    (CustomerId == e.CustomerId);
            }
        }

    }
}
