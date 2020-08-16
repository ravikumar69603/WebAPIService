using System;
using System.Collections.Generic;

namespace WebAPI.Core.Domain
{
    public partial class Customer
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string EmailId { get; set; }
        public string Passwords { get; set; }
        public bool? Published { get; set; }
        public bool? Deleted { get; set; }
        public DateTime? CreateOn { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
    }
}
