﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18063
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace SOA.WLIMS.Web.WCFService.Vehicle {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WCFService.Vehicle.IServiceOf_Vehicle")]
    public interface IServiceOf_Vehicle {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOf_Vehicle/Add", ReplyAction="http://tempuri.org/IServiceOf_Vehicle/AddResponse")]
        bool Add(SOA.WLIMS.Service.DAL.Vehicle model);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOf_Vehicle/Modify", ReplyAction="http://tempuri.org/IServiceOf_Vehicle/ModifyResponse")]
        bool Modify(SOA.WLIMS.Service.DAL.Vehicle model);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOf_Vehicle/Delete", ReplyAction="http://tempuri.org/IServiceOf_Vehicle/DeleteResponse")]
        bool Delete(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOf_Vehicle/Get", ReplyAction="http://tempuri.org/IServiceOf_Vehicle/GetResponse")]
        SOA.WLIMS.Service.DAL.Vehicle Get(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOf_Vehicle/Query", ReplyAction="http://tempuri.org/IServiceOf_Vehicle/QueryResponse")]
        System.Collections.Generic.List<SOA.WLIMS.Service.DAL.Vehicle> Query(SOA.WLIMS.Service.QueryParam para);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceOf_VehicleChannel : SOA.WLIMS.Web.WCFService.Vehicle.IServiceOf_Vehicle, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceOf_VehicleClient : System.ServiceModel.ClientBase<SOA.WLIMS.Web.WCFService.Vehicle.IServiceOf_Vehicle>, SOA.WLIMS.Web.WCFService.Vehicle.IServiceOf_Vehicle {
        
        public ServiceOf_VehicleClient() {
        }
        
        public ServiceOf_VehicleClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceOf_VehicleClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceOf_VehicleClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceOf_VehicleClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool Add(SOA.WLIMS.Service.DAL.Vehicle model) {
            return base.Channel.Add(model);
        }
        
        public bool Modify(SOA.WLIMS.Service.DAL.Vehicle model) {
            return base.Channel.Modify(model);
        }
        
        public bool Delete(int id) {
            return base.Channel.Delete(id);
        }
        
        public SOA.WLIMS.Service.DAL.Vehicle Get(int id) {
            return base.Channel.Get(id);
        }
        
        public System.Collections.Generic.List<SOA.WLIMS.Service.DAL.Vehicle> Query(SOA.WLIMS.Service.QueryParam para) {
            return base.Channel.Query(para);
        }
    }
}
