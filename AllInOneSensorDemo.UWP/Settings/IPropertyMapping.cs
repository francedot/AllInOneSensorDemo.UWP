using System;

namespace AllInOneSensorDemo.UWP.Settings
{
    public interface IPropertyMapping
    {
        IStoreConverter GetConverter(Type type);
    }
}