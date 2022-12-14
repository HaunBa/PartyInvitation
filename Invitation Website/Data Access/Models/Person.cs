using System;
using System.Collections.Generic;

namespace Data_Access.Models
{
    public partial class Person
    {
        public Person()
        {
            RequestRequests = new HashSet<Request>();
        }

        public int PersonId { get; set; } // generated by db
        public string PersonName { get; set; } = null!; // input
        public string PersonNameHashed { get; set; } = null!; // generated by code
        public int PersonRequestState { get; set; } // default 0

        public virtual ICollection<Request> RequestRequests { get; set; }
    }
}
