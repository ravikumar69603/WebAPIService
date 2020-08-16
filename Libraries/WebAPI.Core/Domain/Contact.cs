using System;
using System.Collections.Generic;

namespace WebAPI.Core.Domain
{
    public partial class Contact
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string EmailId { get; set; }
        public string Message { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
    }
}
