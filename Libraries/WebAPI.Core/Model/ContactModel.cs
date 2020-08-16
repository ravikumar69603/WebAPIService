using System;
using System.Collections.Generic;
using System.Text;

namespace WebAPI.Core.Model
{
    public class ContactModel : BaseEntity
    {        
        public string FullName { get; set; }
        public string EmailId { get; set; }
        public string Message { get; set; }
    }
}
