using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SOA.WLIMS.Contract;

namespace SOA.WLIMS.BLL
{
    public class UserBLL:IUserService
    {
        public int MinPasswordLength()
        {
            throw new NotImplementedException();
        }

        public bool ValidateUser(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public System.Web.Security.MembershipCreateStatus CreateUser(SOA.WLIMS.Models.RegisterModel model)
        {
            throw new NotImplementedException();
        }

        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public bool Modify(SOA.WLIMS.Models.UserModel model)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public SOA.WLIMS.Models.UserModel Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<SOA.WLIMS.Models.UserModel> Query(SOA.WLIMS.Models.QueryParam param)
        {
            throw new NotImplementedException();
        }
    }
}
