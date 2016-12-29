using System;

namespace AllInOneSensorDemo.UWP.Settings
{
    public interface IStoreConverter
    {
        string ToStore(object value, Type type);
        object FromStore(string value, Type type);
    }
}