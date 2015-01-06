using System;
using SOA.WLIMS.Contract;
using System.ServiceModel;
using SOA.WLIMS.Utility;
using System.Xml;
using System.ServiceModel.Description;

namespace SOA.WLIMS.WCFClientProxy
{
    public class RemoteServiceFactory : IServiceFactory
    {
        //need to get it from a config file after implemented a dynamic config manage module
        
        private readonly string userServiceUri = "http://localhost:1735/UserService.svc";

        public string vehicleServiceUri = "http://localhost:1735/VehicleService.svc";
        public string orderServiceUri = "http://localhost:1735/OrderService.svc";
        public string storehouseServiceUri = "http://localhost:1735/StoreService.svc";
                
        private T CreateService<T>(string uri)
        {
            var key = string.Format("{0} - {1}", typeof(T), uri);

            if (Caching.Get(key) == null)
            {
                var binding = new BasicHttpBinding();
                binding.MaxReceivedMessageSize = maxReceivedMessageSize;
                binding.ReaderQuotas = new XmlDictionaryReaderQuotas();
                binding.ReaderQuotas.MaxStringContentLength = maxReceivedMessageSize;
                binding.ReaderQuotas.MaxArrayLength = maxReceivedMessageSize;
                binding.ReaderQuotas.MaxBytesPerRead = maxReceivedMessageSize;

                ChannelFactory<T> chan = new ChannelFactory<T>(binding, new EndpointAddress(uri));
                chan.Endpoint.Behaviors.Add(new SOA.WLIMS.WCFExtension.InspectorBehavior());
                foreach (var op in chan.Endpoint.Contract.Operations)
                {
                    var dataContractBehavior = op.Behaviors.Find<DataContractSerializerOperationBehavior>() as DataContractSerializerOperationBehavior;
                    if (dataContractBehavior != null)
                        dataContractBehavior.MaxItemsInObjectGraph = int.MaxValue;
                }

                chan.Open();

                var service = chan.CreateChannel();
                Caching.Set(key, service);

                return service;
            }
            else
            {
                return (T)Caching.Get(key);
            }
        }

        private const int maxReceivedMessageSize = 2147483647;


        public IUserService GetUserService()
        {
            return this.CreateService<IUserService>(userServiceUri);
        }

        public IService<Models.VehicleModel> GetVehicleService()
        {
            return this.CreateService<IService<Models.VehicleModel>>(vehicleServiceUri);
        }

        public IOrderService GetOrderService()
        {
            return this.CreateService<IOrderService>(orderServiceUri);
        }

        public IService<Models.StorehouseModel> GetStorehouseService()
        {
            return this.CreateService<IService<Models.StorehouseModel>>(storehouseServiceUri);
        }

    }
}
