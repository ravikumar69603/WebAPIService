using System;
using System.Collections.Generic;
using System.Text;
using WebAPI.Core.Domain;
using System.Linq;
using WebAPI.Core.Model;

namespace WebAPI.Service.User
{
    public partial class UserServices : IUserServices
    {
        private readonly EmployeeContext _context;

        public UserServices(EmployeeContext context)
        {
            _context = context;
        }


        public UserWithToken UserVaild(string emailId, string password)
        {
            var model = new UserWithToken();

            var data = _context.Customer.Where(a => a.EmailId == emailId && a.Passwords == password).FirstOrDefault();

            if (data != null)
            {
                model.UserId = data.Id;
                model.FullName = data.FullName;
                model.UserEmailId = data.EmailId;
                model.IsValid = true;

                return model;
            }

            
            return model;
        }
    }
}
