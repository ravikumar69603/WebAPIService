using System;
using System.Collections.Generic;
using System.Linq;
using WebAPI.Core;
using WebAPI.Core.Domain;
using WebAPI.Core.Model;

namespace WebAPI.Service.Contacts
{
    public class ContactServices : IContactServices
    {
        private readonly EmployeeContext _context;

        public ContactServices(EmployeeContext context)
        {
            _context = context;
        }

        public IEnumerable<ContactModel> GetContactList()
        {
            var contactList = (from c in _context.Contact
                           select new ContactModel
                           {
                               Id = c.Id,
                               FullName = c.FullName,
                               EmailId = c.EmailId,
                               Message = c.Message
                           }).ToList();

            return contactList;
        }

        public ContactPagingList GetContactPagingList(int currentPage = 1, int pageSize = 10)
        {
            var model = new ContactPagingList();

            var contactList = (from c in _context.Contact
                               select new ContactModel
                               {
                                   Id = c.Id,
                                   FullName = c.FullName,
                                   EmailId = c.EmailId,
                                   Message = c.Message
                               }).OrderBy(a => a.Id).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            model.ContactList = contactList;

            int totalRecord = _context.Contact.Count();

            var page = new Pagination
            {
                Count = totalRecord,
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(decimal.Divide(totalRecord, pageSize)),
                IndexOne = ((currentPage - 1) * pageSize + 1),
                IndexTwo = (((currentPage - 1) * pageSize + pageSize) <= totalRecord ? ((currentPage - 1) * pageSize + pageSize) : totalRecord)
            };
            model.Pagination = page;

            return model;
        }


        public bool AddContactDetails(ContactModel model)
        {
            using(var transcation = _context.Database.BeginTransaction())
            {
                try
                {
                    var contact = new Contact
                    {
                        FullName = model.FullName,
                        EmailId = model.EmailId,
                        Message = model.Message,
                        CreatedOn = DateTime.UtcNow,
                        LastUpdatedOn = DateTime.UtcNow
                    };

                    _context.Contact.Add(contact);
                    _context.SaveChanges();
                    transcation.Commit();
                    return true;
                }
                catch (Exception)
                {
                    transcation.Rollback();
                    return false;
                }

            }

        }

        public ContactModel GetContactById(int Id)
        {
            var contact = _context.Contact.FirstOrDefault(a=>a.Id == Id);

            if (contact != null)
            {
                var model = new ContactModel
                {
                    Id = contact.Id,
                    EmailId = contact.EmailId,
                    FullName = contact.FullName,
                    Message = contact.Message
                };

                return model;
            }

            return new ContactModel();
        }

        public bool EditContact(ContactModel model)
        {
            using(var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var data = _context.Contact.FirstOrDefault(a => a.Id == model.Id);

                    if (data != null)
                    {
                        data.FullName = model.FullName;
                        data.EmailId = model.EmailId;
                        data.Message = model.Message;
                        data.LastUpdatedOn = DateTime.UtcNow;

                        _context.Contact.Update(data);
                        _context.SaveChanges();
                        transaction.Commit();
                        return true;
                    }

                    return false;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
                               

            }
        }

        public bool DeleteContact(int Id)
        {
            using(var transcation = _context.Database.BeginTransaction())
            {
                try
                {
                    var data = _context.Contact.FirstOrDefault(a => a.Id == Id);

                    if (data != null)
                    {
                        _context.Contact.Remove(data);
                        _context.SaveChanges();
                        transcation.Commit();

                        return true;
                    }

                    return false;
                }
                catch (Exception)
                {
                    transcation.Rollback();
                    return false;
                }
            }
        }
       

    }
}
