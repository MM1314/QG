using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using WX.Framework;
using WX.Model;
using WX.Demo.WebClasses.Roles;

namespace WX.Demo.WebClasses
{
    public class LocationMessageRole : IMessageRole
    {
        public IMessageHandler MessageRole(MiddleMessage msg)
        {            
            return new LocationEventMessageHandler(msg);
        }

      
    }
}