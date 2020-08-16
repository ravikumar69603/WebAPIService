using System;
using System.Collections.Generic;
using System.Text;

namespace WebAPI.Core.Model
{
    public class UserModel
    {
        public string EmailId { get; set; }
        public string Password { get; set; }
    }

    public class UserWithToken
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public bool IsValid { get; set; }
        public string UserEmailId { get; set; }
        public string AccessToken { get; set; }
    }
}
