using System;
using GalaSoft.MvvmLight.Views;

namespace AllInOneSensorDemo.UWP.Services
{
    public interface IExtendedNavigationService : INavigationService
    {
        void NavigateTo(Type pageType);
    }
}