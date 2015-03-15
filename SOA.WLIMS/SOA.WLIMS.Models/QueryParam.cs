using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Runtime.Serialization;

namespace SOA.WLIMS.Models
{
    /// <summary>
    /// 查询参数
    /// </summary>
    [DataContract]
    public class QueryParam
    {
        bool? boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool? BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
        [DataMember]
        public Dictionary<string, string> Param { get; set; }
    }
}
