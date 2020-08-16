using System;
using System.Collections.Generic;
using System.Text;

namespace WebAPI.Core.Model
{
    public class ContactPagingList
    {
        public Pagination Pagination { get; set; }
        public List<ContactModel> ContactList { get; set; }
    }
}
