using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using SOA.WLIMS.Models;
using System.Web.Security;
using SOA.WLIMS.DAL;
using SOA.WLIMS.Contract;

namespace SOA.WLIMS.Service
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“UserService”。
    public class UserService : IUserService
    {
        SOAWLDBEntities DB = new SOAWLDBEntities();


        public int MinPasswordLength()
        {
            return 8;
        }


        public List<UserModel> Query(QueryParam param)
        {
            return DB.User.Select(model => new UserModel()
            {
                ID = model.ID,
                Name = model.Name,
                AliasName = model.AliasName,
                Email = model.Email,
                Password = model.Password,
                Role = model.Role,
                EmployeCode = model.EmployeCode,
                Enable = model.Enable,
                IsDeleted = model.IsDeleted
            }).ToList();
        }

        public bool ValidateUser(string userName, string password)
        {
            return DB.User.Where(o => o.Name == userName.Trim() && o.Password == password.Trim()).Count() == 1;
        }

        public MembershipCreateStatus CreateUser(RegisterModel model)
        {
            try
            {
                DB.User.AddObject(new User()
                {
                    Name = model.UserName,
                    AliasName = model.UserName,
                    Email = model.Email,
                    Password = model.Password,
                    Role = model.Role,
                    EmployeCode = model.EmployeCode,
                    Enable = true,
                    IsDeleted = false
                });
                DB.SaveChanges();
                return System.Web.Security.MembershipCreateStatus.Success;
            }
            catch (Exception ex)
            {
                return System.Web.Security.MembershipCreateStatus.ProviderError;
            }
        }

        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("值不能为 null 或为空。", "userName");
            if (String.IsNullOrEmpty(oldPassword)) throw new ArgumentException("值不能为 null 或为空。", "oldPassword");
            if (String.IsNullOrEmpty(newPassword)) throw new ArgumentException("值不能为 null 或为空。", "newPassword");


            User currentUser = DB.User.SingleOrDefault(o => o.Name == userName);
            if (currentUser == null)
            {
                return false;
            }
            if (currentUser.Password != oldPassword)
            {
                return false;
            }
            currentUser.Password = newPassword;
            return DB.SaveChanges() == 1;
        }

        public bool Modify(UserModel model)
        {
            User user = DB.User.SingleOrDefault(o => o.ID == model.ID);
            if (user != null)
            {
                user.AliasName = model.AliasName;
                user.Email = model.Email;
                user.Tel = model.Tel;
                user.Role = model.Role;
                user.EmployeCode = model.EmployeCode;
                user.IsDeleted = model.IsDeleted;
                user.Enable = model.Enable;
            }
            return DB.SaveChanges() == 1;
        }
        public bool Delete(int id)
        {
            try
            {
                DB.User.SingleOrDefault(o => o.ID == id).IsDeleted = true;
                return DB.SaveChanges() == 1;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public UserModel Get(int id)
        {
            return DB.User.Select(model => new UserModel()
            {
                ID = model.ID,
                Name = model.Name,
                AliasName = model.AliasName,
                Email = model.Email,
                Password = model.Password,
                Role = model.Role,
                EmployeCode = model.EmployeCode,
                Enable = model.Enable,
                IsDeleted = model.IsDeleted
            }).SingleOrDefault(o => o.ID == id);
        }

    }
}
