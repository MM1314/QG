using System;
using EasyEF.Contract;
using System.ServiceModel;
using EasyEF.Utility;
using System.Xml;
using System.ServiceModel.Description;

namespace EasyEF.WCFClientProxy
{
    public class RemoteServiceFactory : IServiceFactory
    {
        //need to get it from a config file after implemented a dynamic config manage module
        private readonly string serviceUri = "http://localhost:4000/Service.svc";
        
        public IService CreateService()
        {
            return this.CreateService<IService>(serviceUri);
        }

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
                chan.Endpoint.Behaviors.Add(new EasyEF.WCFExtension.InspectorBehavior());
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
    }
}
