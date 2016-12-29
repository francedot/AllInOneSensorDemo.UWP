using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using AllInOneSensorDemo.UWP.Helpers;
using AllInOneSensorDemo.UWP.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;

namespace AllInOneSensorDemo.UWP.ViewModels
{
    public class RootPageViewModel : GalaSoft.MvvmLight.ViewModelBase
    {
        private readonly IBandService _iBandService;
        private readonly IExtendedNavigationService _iNavigationService;

        public RootPageViewModel(IBandService iBandService, IExtendedNavigationService iNavigationService)
        {
            _iBandService = iBandService;
            _iNavigationService = iNavigationService;
        }

        public async Task<bool> BootStrap()
        {
            return await _iBandService.Init();
        }

        public async Task OnNavigatedTo()
        {
            var connected = false;
            do
            {
                connected = await BootStrap();
                if (connected)
                    _iNavigationService.NavigateTo(typeof(Views.GeneralInfoPage));
                else
                {
                    var dialog = new MessageDialog("No band has been found. Do you want to retry?");
                    dialog.Commands.Add(new UICommand("Yes"));
                    dialog.Commands.Add(new UICommand("No"));
                    var result = await dialog.ShowAsync();
                    if (result.Label == "No")
                        Application.Current.Exit();
                }
            } while (!connected);
        }

        private DelegateCommand<string> _navigationCommand = default(DelegateCommand<string>);
        public DelegateCommand<string> NavigationCommand
        {
            get
            {
                return _navigationCommand ??
                       (_navigationCommand =
                           new DelegateCommand<string>(ExecuteNavigationCommand, CanExecuteNavigationCommand));
            }
            set { _navigationCommand = value; }
        }

        private static bool CanExecuteNavigationCommand(string parameter) { return true; }
        public void ExecuteNavigationCommand(string parameter)
        {
            try
            {
                _iNavigationService.NavigateTo(ViewModelLocator.ViewsAssemblyName + parameter);
            }
            catch
            {
                Debugger.Break();
            }
        }
    }
}