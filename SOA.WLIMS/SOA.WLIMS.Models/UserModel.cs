using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOA.WLIMS.Models
{
    public class UserModel
    {
        
        public global::System.Int64 ID
        {
            get;
            set;
        }
        
        public global::System.String Name
        {
            get;
            set;
        }
        
        public global::System.String AliasName
        {
            get;
            set;
        }
        
        public global::System.String Password
        {
            get;
            set;
        }
        
        public global::System.String Tel
        {
            get;
            set;
        }
        
        public global::System.String Email
        {
            get;
            set;
        }
        
        public global::System.String Role
        {
            get;
            set;
        }
        
        public global::System.String EmployeCode
        {
            get;
            set;
        }
        

        public Nullable<global::System.Boolean> Enable
        {
            get;
            set;
        }
        
        public Nullable<global::System.Boolean> IsDeleted
        {
            get;
            set;
        }

        public Nullable<global::System.DateTime> LastActivityDate
        {
            get;
            set;
        }
      
    }
}
