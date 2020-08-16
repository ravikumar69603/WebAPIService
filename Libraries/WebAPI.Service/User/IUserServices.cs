using System;
using System.Collections.Generic;
using System.Text;
using WebAPI.Core.Model;

namespace WebAPI.Service.User
{
    public interface IUserServices
    {
        UserWithToken UserVaild(string emailId, string password);


    }
}
