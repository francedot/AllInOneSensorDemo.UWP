using System;
using System.Diagnostics;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using AllInOneSensorDemo.UWP.Helpers;
using AllInOneSensorDemo.UWP.Services;
using AllInOneSensorDemo.UWP.ViewModels;
using GalaSoft.MvvmLight.Ioc;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AllInOneSensorDemo.UWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RootPage : Page
    {
        public RootPageViewModel RootPageViewModel
            => ((ViewModelLocator)Application.Current.Resources["ViewModelLocator"]).RootPageViewModel;

        public static Frame InnerFrame { get; set; }

        public RootPage()
        {
            this.InitializeComponent();
            InnerFrame = ContentFrame;
        }

        public bool IsHamburgerMenuOpen
        {
            get
            {
                return ShellSplitView.IsPaneOpen;

            }
            set
            {
                ShellSplitView.IsPaneOpen = value;

            }
        }

        private void BackRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var pageName = ((RadioButton)sender);

            //switch ()
            //{

            //}
        }

        private void HamburgerRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            // don't let the radiobutton check
            ((RadioButton)sender).IsChecked = false;

            // toggle the splitview pane
            this.ShellSplitView.IsPaneOpen = !this.ShellSplitView.IsPaneOpen;
        }

        private void ShellSplitView_OnPointerExited(object sender, PointerRoutedEventArgs e)
        {
            if (VisualStateGroup.CurrentState != null && (ShellSplitView.IsPaneOpen && !VisualStateGroup.CurrentState.Name.Equals("VisualStateWide")))
                ShellSplitView.IsPaneOpen = false;
        }

        private void IsTypePresentStateTriggers_OnCurrentStateChanged(object sender, VisualStateChangedEventArgs e)
        {
            Debug.WriteLine("Change");
        }

        private void NavigationButton_OnChecked(object sender, RoutedEventArgs e)
        {
            var navButton = ((RadioButton)sender);
            RootPageViewModel.ExecuteNavigationCommand(navButton.CommandParameter?.ToString());
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            var navService = new NavigationService(InnerFrame);
            SimpleIoc.Default.Register<IExtendedNavigationService>(() => navService);
            await RootPageViewModel.OnNavigatedTo();
            base.OnNavigatedTo(e);
        }
    }
}
