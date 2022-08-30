using System;
using System.Collections.Generic;

namespace Data_Access.Models
{
    public partial class Request
    {
        public Request()
        {
            PersonPeople = new HashSet<Person>();
        }

        public int RequestId { get; set; }
        public string RequestArticle { get; set; } = null!;
        public int RequestAmount { get; set; }

        public virtual ICollection<Person> PersonPeople { get; set; }
    }
}
