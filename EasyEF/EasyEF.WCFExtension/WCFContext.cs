using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting.Messaging;
using System.Web;
using EasyEF.Utility;

namespace EasyEF.WCFExtension
{
    [Serializable]
    public class WCFContext:Dictionary<string,object>
    {
        private const string CallContextKey = "__WCFContext";    
        internal const string ContextHeaderLocalName = "__WCFContext";
        internal const string ContextHeaderNamespace = "urn:easyef.com";

        private void EnsureSerializable(object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            if (!value.GetType().IsSerializable)
            {
                throw new ArgumentException(string.Format("The argument of the type \"{0}\" is not serializable!", value.GetType().FullName));
            }
        }       

        public new  object this[string key]
        {
            get
            {
                return base[key];
            }
            set
            {
                this.EnsureSerializable(value);
                base[key] = value;
            }
        }

        public Operater Operater
        {
            get
            {
                return SerializeHelper.JsonDeserialize<Operater>(this["__Operater"].ToString());
            }
            set
            {
                this["__Operater"] = SerializeHelper.JsonSerialize<Operater>(value);
            }
        } 

        public static WCFContext Current
        {
            get
            {
                if (CallContext.GetData(CallContextKey) == null)
                { 
                    CallContext.SetData(CallContextKey, new WCFContext());
                }

                return CallContext.GetData(CallContextKey) as WCFContext;
            }
            set
            {
                CallContext.SetData(CallContextKey, value);
            }
        }     
    }

    public class Operater
    {
        public string Name { get; set; }
        public string IP { get; set; }
        public DateTime Time { get; set; }
        public Guid LoginToken { get; set; }
        public string Method { get; set; }

    }
}
