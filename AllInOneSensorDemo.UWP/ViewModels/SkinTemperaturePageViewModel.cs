using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using AllInOneSensorDemo.UWP.Services;
using GalaSoft.MvvmLight;
using Microsoft.Band.Sensors;

namespace AllInOneSensorDemo.UWP.ViewModels
{
    public class SkinTemperaturePageViewModel : GalaSoft.MvvmLight.ViewModelBase
    {
        private readonly IBandService _iBandService;

        private IBandSensor<IBandSkinTemperatureReading> _skinTemperature;

        private double _temperature;
        public double Temperature
        {
            get { return _temperature; }
            set { Set(ref _temperature, value); }
        }


        public SkinTemperaturePageViewModel(IBandService iBandService)
        {
            _iBandService = iBandService;
            if (GalaSoft.MvvmLight.ViewModelBase.IsInDesignModeStatic)
            {
                Temperature = 37.4;
            }
        }

        private async void SkinTemperatureOnReadingChanged(object sender, BandSensorReadingEventArgs<IBandSkinTemperatureReading> e)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                 () =>
                 {
                     Temperature = e.SensorReading.Temperature;
                 });
        }

        public async Task OnNavigatedTo()
        {
            _skinTemperature = _iBandService.GetSkinTemperature(); // TODO tutti cosi
            _skinTemperature.ReadingChanged += SkinTemperatureOnReadingChanged;
            await _iBandService.StartReadingSkinTempearture();
        }

        public void OnNavigatedFrom()
        {
            _skinTemperature.ReadingChanged -= SkinTemperatureOnReadingChanged;
            _iBandService.StopReadingSkinTempearture();
        }
    }
}