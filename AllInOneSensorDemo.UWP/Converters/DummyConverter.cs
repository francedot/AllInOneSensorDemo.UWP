using System;
using System.Diagnostics;
using Windows.UI.Xaml.Data;

namespace AllInOneSensorDemo.UWP.Converters
{
    public class DummyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Debugger.Break();
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}