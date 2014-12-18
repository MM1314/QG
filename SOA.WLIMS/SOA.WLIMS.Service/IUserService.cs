using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web.Security;
using SOA.WLIMS.Models;
using SOA.WLIMS.Service.DAL;

namespace SOA.WLIMS.Service
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IUserService”。

    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        int MinPasswordLength();
        [OperationContract]
        bool ValidateUser(string userName, string password);
        [OperationContract]
        MembershipCreateStatus CreateUser(RegisterModel model);
        [OperationContract]
        bool ChangePassword(string userName, string oldPassword, string newPassword);


        [OperationContract]
        bool Modify(User model);
        [OperationContract]
        bool Delete(int id);
        [OperationContract]
        User Get(int id);
        [OperationContract]
        List<User> Query(QueryParam param);
    }
}
