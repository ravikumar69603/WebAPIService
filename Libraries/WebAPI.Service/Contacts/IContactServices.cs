using System;
using System.Collections.Generic;
using System.Text;
using WebAPI.Core.Model;

namespace WebAPI.Service.Contacts
{
    public interface IContactServices
    {
        IEnumerable<ContactModel> GetContactList();
        ContactPagingList GetContactPagingList(int currentPage = 1, int pageSize = 10);
        bool AddContactDetails(ContactModel model);

        ContactModel GetContactById(int Id);

        bool EditContact(ContactModel model);
        bool DeleteContact(int Id);
    }
}
