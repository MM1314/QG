using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SOA.WLIMS.Service.DAL;

namespace SOA.WLIMS.Service
{
    public class DBFactory
    {
        public static SOAWLDBEntities CreateDB { get { return new SOAWLDBEntities(); } }
    }
}