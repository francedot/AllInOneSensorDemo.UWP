using System.Threading.Tasks;
using AllInOneSensorDemo.UWP.Services;
using GalaSoft.MvvmLight;

namespace AllInOneSensorDemo.UWP.ViewModels
{
    public class GeneralInfoPageViewModel : GalaSoft.MvvmLight.ViewModelBase
    {
        private string _bandName;
        public string BandName
        {
            get { return _bandName; }
            set { Set(ref _bandName, value); }
        }

        private string _firmwareVersion;
        public string FirmwareVersion
        {
            get { return _firmwareVersion; }
            set { Set(ref _firmwareVersion, value); }
        }

        private string _hardwareVersion;
        private readonly IBandService _iBandService;

        public string HardwareVersion
        {
            get { return _hardwareVersion; }
            set { Set(ref _hardwareVersion, value); }
        }

        public GeneralInfoPageViewModel(IBandService iBandService)
        {
            _iBandService = iBandService;

            if (GalaSoft.MvvmLight.ViewModelBase.IsInDesignModeStatic)
            {
                BandName = "Band di Francesco";
                FirmwareVersion = "40.5.4.3.22";
                HardwareVersion = "9";
            }

            BandName = _iBandService.GetBandName();
        }

        public async Task OnNavigatedTo()
        {
            FirmwareVersion = await _iBandService.GetFirmwareVersion();
            HardwareVersion = await _iBandService.GetHardwareVersion();
        }
    }
}