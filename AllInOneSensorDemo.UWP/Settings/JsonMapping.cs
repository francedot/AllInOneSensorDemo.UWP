using System;

namespace AllInOneSensorDemo.UWP.Settings
{
    public class JsonMapping : IPropertyMapping
    {
        protected IStoreConverter jsonConverter = new JsonConverter();
        public IStoreConverter GetConverter(Type type) => this.jsonConverter;
    }
}