﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18063
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace SOA.WLIMS.Web.WCFService.Order {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="StructuralObject", Namespace="http://schemas.datacontract.org/2004/07/System.Data.Objects.DataClasses", IsReference=true)]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(SOA.WLIMS.Web.WCFService.Order.EntityObject))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(SOA.WLIMS.Web.WCFService.Order.Order))]
    public partial class StructuralObject : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="EntityObject", Namespace="http://schemas.datacontract.org/2004/07/System.Data.Objects.DataClasses", IsReference=true)]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(SOA.WLIMS.Web.WCFService.Order.Order))]
    public partial class EntityObject : SOA.WLIMS.Web.WCFService.Order.StructuralObject {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private SOA.WLIMS.Web.WCFService.Order.EntityKey EntityKeyField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public SOA.WLIMS.Web.WCFService.Order.EntityKey EntityKey {
            get {
                return this.EntityKeyField;
            }
            set {
                if ((object.ReferenceEquals(this.EntityKeyField, value) != true)) {
                    this.EntityKeyField = value;
                    this.RaisePropertyChanged("EntityKey");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Order", Namespace="http://schemas.datacontract.org/2004/07/SOA.WLIMS.DAL", IsReference=true)]
    [System.SerializableAttribute()]
    public partial class Order : SOA.WLIMS.Web.WCFService.Order.EntityObject {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<decimal> ChargesField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CodeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ContentsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<decimal> ContentsValueField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double ContentsWeightField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime PickupTimeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<System.DateTime> SigninTimeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SrcAddressField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SrcUserNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SrcUserPhoneField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StatusField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ToAddressField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ToUserNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ToUserPhoneField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<decimal> Charges {
            get {
                return this.ChargesField;
            }
            set {
                if ((this.ChargesField.Equals(value) != true)) {
                    this.ChargesField = value;
                    this.RaisePropertyChanged("Charges");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Code {
            get {
                return this.CodeField;
            }
            set {
                if ((object.ReferenceEquals(this.CodeField, value) != true)) {
                    this.CodeField = value;
                    this.RaisePropertyChanged("Code");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Contents {
            get {
                return this.ContentsField;
            }
            set {
                if ((object.ReferenceEquals(this.ContentsField, value) != true)) {
                    this.ContentsField = value;
                    this.RaisePropertyChanged("Contents");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<decimal> ContentsValue {
            get {
                return this.ContentsValueField;
            }
            set {
                if ((this.ContentsValueField.Equals(value) != true)) {
                    this.ContentsValueField = value;
                    this.RaisePropertyChanged("ContentsValue");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double ContentsWeight {
            get {
                return this.ContentsWeightField;
            }
            set {
                if ((this.ContentsWeightField.Equals(value) != true)) {
                    this.ContentsWeightField = value;
                    this.RaisePropertyChanged("ContentsWeight");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long ID {
            get {
                return this.IDField;
            }
            set {
                if ((this.IDField.Equals(value) != true)) {
                    this.IDField = value;
                    this.RaisePropertyChanged("ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime PickupTime {
            get {
                return this.PickupTimeField;
            }
            set {
                if ((this.PickupTimeField.Equals(value) != true)) {
                    this.PickupTimeField = value;
                    this.RaisePropertyChanged("PickupTime");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> SigninTime {
            get {
                return this.SigninTimeField;
            }
            set {
                if ((this.SigninTimeField.Equals(value) != true)) {
                    this.SigninTimeField = value;
                    this.RaisePropertyChanged("SigninTime");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SrcAddress {
            get {
                return this.SrcAddressField;
            }
            set {
                if ((object.ReferenceEquals(this.SrcAddressField, value) != true)) {
                    this.SrcAddressField = value;
                    this.RaisePropertyChanged("SrcAddress");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SrcUserName {
            get {
                return this.SrcUserNameField;
            }
            set {
                if ((object.ReferenceEquals(this.SrcUserNameField, value) != true)) {
                    this.SrcUserNameField = value;
                    this.RaisePropertyChanged("SrcUserName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SrcUserPhone {
            get {
                return this.SrcUserPhoneField;
            }
            set {
                if ((object.ReferenceEquals(this.SrcUserPhoneField, value) != true)) {
                    this.SrcUserPhoneField = value;
                    this.RaisePropertyChanged("SrcUserPhone");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Status {
            get {
                return this.StatusField;
            }
            set {
                if ((object.ReferenceEquals(this.StatusField, value) != true)) {
                    this.StatusField = value;
                    this.RaisePropertyChanged("Status");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ToAddress {
            get {
                return this.ToAddressField;
            }
            set {
                if ((object.ReferenceEquals(this.ToAddressField, value) != true)) {
                    this.ToAddressField = value;
                    this.RaisePropertyChanged("ToAddress");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ToUserName {
            get {
                return this.ToUserNameField;
            }
            set {
                if ((object.ReferenceEquals(this.ToUserNameField, value) != true)) {
                    this.ToUserNameField = value;
                    this.RaisePropertyChanged("ToUserName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ToUserPhone {
            get {
                return this.ToUserPhoneField;
            }
            set {
                if ((object.ReferenceEquals(this.ToUserPhoneField, value) != true)) {
                    this.ToUserPhoneField = value;
                    this.RaisePropertyChanged("ToUserPhone");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="EntityKey", Namespace="http://schemas.datacontract.org/2004/07/System.Data", IsReference=true)]
    [System.SerializableAttribute()]
    public partial class EntityKey : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EntityContainerNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.List<SOA.WLIMS.Web.WCFService.Order.EntityKeyMember> EntityKeyValuesField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EntitySetNameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string EntityContainerName {
            get {
                return this.EntityContainerNameField;
            }
            set {
                if ((object.ReferenceEquals(this.EntityContainerNameField, value) != true)) {
                    this.EntityContainerNameField = value;
                    this.RaisePropertyChanged("EntityContainerName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.List<SOA.WLIMS.Web.WCFService.Order.EntityKeyMember> EntityKeyValues {
            get {
                return this.EntityKeyValuesField;
            }
            set {
                if ((object.ReferenceEquals(this.EntityKeyValuesField, value) != true)) {
                    this.EntityKeyValuesField = value;
                    this.RaisePropertyChanged("EntityKeyValues");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string EntitySetName {
            get {
                return this.EntitySetNameField;
            }
            set {
                if ((object.ReferenceEquals(this.EntitySetNameField, value) != true)) {
                    this.EntitySetNameField = value;
                    this.RaisePropertyChanged("EntitySetName");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="EntityKeyMember", Namespace="http://schemas.datacontract.org/2004/07/System.Data")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(System.Collections.Generic.Dictionary<string, string>))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(SOA.WLIMS.Models.QueryParam))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(SOA.WLIMS.Web.WCFService.Order.EntityObject))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(SOA.WLIMS.Web.WCFService.Order.StructuralObject))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(SOA.WLIMS.Web.WCFService.Order.EntityKey))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(System.Collections.Generic.List<SOA.WLIMS.Web.WCFService.Order.EntityKeyMember>))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(SOA.WLIMS.Web.WCFService.Order.Order))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(System.Collections.Generic.List<SOA.WLIMS.Web.WCFService.Order.Order>))]
    public partial class EntityKeyMember : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string KeyField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private object ValueField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Key {
            get {
                return this.KeyField;
            }
            set {
                if ((object.ReferenceEquals(this.KeyField, value) != true)) {
                    this.KeyField = value;
                    this.RaisePropertyChanged("Key");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public object Value {
            get {
                return this.ValueField;
            }
            set {
                if ((object.ReferenceEquals(this.ValueField, value) != true)) {
                    this.ValueField = value;
                    this.RaisePropertyChanged("Value");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WCFService.Order.IOrderService")]
    public interface IOrderService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderService/Add", ReplyAction="http://tempuri.org/IOrderService/AddResponse")]
        bool Add(SOA.WLIMS.Web.WCFService.Order.Order order);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderService/Modify", ReplyAction="http://tempuri.org/IOrderService/ModifyResponse")]
        bool Modify(SOA.WLIMS.Web.WCFService.Order.Order order);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderService/Delete", ReplyAction="http://tempuri.org/IOrderService/DeleteResponse")]
        bool Delete(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderService/Get", ReplyAction="http://tempuri.org/IOrderService/GetResponse")]
        SOA.WLIMS.Web.WCFService.Order.Order Get(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderService/Query", ReplyAction="http://tempuri.org/IOrderService/QueryResponse")]
        System.Collections.Generic.List<SOA.WLIMS.Web.WCFService.Order.Order> Query(SOA.WLIMS.Models.QueryParam para);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IOrderServiceChannel : SOA.WLIMS.Web.WCFService.Order.IOrderService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class OrderServiceClient : System.ServiceModel.ClientBase<SOA.WLIMS.Web.WCFService.Order.IOrderService>, SOA.WLIMS.Web.WCFService.Order.IOrderService {
        
        public OrderServiceClient() {
        }
        
        public OrderServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public OrderServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public OrderServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public OrderServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool Add(SOA.WLIMS.Web.WCFService.Order.Order order) {
            return base.Channel.Add(order);
        }
        
        public bool Modify(SOA.WLIMS.Web.WCFService.Order.Order order) {
            return base.Channel.Modify(order);
        }
        
        public bool Delete(int id) {
            return base.Channel.Delete(id);
        }
        
        public SOA.WLIMS.Web.WCFService.Order.Order Get(int id) {
            return base.Channel.Get(id);
        }
        
        public System.Collections.Generic.List<SOA.WLIMS.Web.WCFService.Order.Order> Query(SOA.WLIMS.Models.QueryParam para) {
            return base.Channel.Query(para);
        }
    }
}
