using System;
using System.Diagnostics;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using GalaSoft.MvvmLight.Views;

namespace AllInOneSensorDemo.UWP.Services
{
    public class NavigationService : IExtendedNavigationService
    {
        private readonly Frame _navigationFrame;

        public NavigationService(Frame innerFrame)
        {
            _navigationFrame = innerFrame;
        }

        public void GoBack()
        {
            _navigationFrame.GoBack(new DrillInNavigationTransitionInfo());
        }

        public void NavigateTo(string pageKey)
        {
            NavigateTo(pageKey, null);
        }

        public void NavigateTo(Type pageType)
        {
            _navigationFrame.Navigate(pageType, null);
        }

        public void NavigateTo(string pageKey, object parameter)
        {
            try
            {
                var pageType = Type.GetType(pageKey);
                _navigationFrame.Navigate(pageType, parameter, new DrillInNavigationTransitionInfo());
            }
            catch
            {
                Debugger.Break();
            }
        }

        public string CurrentPageKey { get; }
    }
}