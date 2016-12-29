using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using AllInOneSensorDemo.UWP.Services;
using GalaSoft.MvvmLight;
using Microsoft.Band.Sensors;

namespace AllInOneSensorDemo.UWP.ViewModels
{
    public class HeartRatePageViewModel : GalaSoft.MvvmLight.ViewModelBase
    {
        private readonly IBandService _iBandService;

        private IBandSensor<IBandHeartRateReading> _heartRate;

        private string _quality;
        public string Quality
        {
            get { return _quality; }
            set { Set(ref _quality, value); }
        }

        private int _rate;

        public int Rate
        {
            get { return _rate; }
            set { Set(ref _rate, value); }
        }

        public HeartRatePageViewModel(IBandService iBandService)
        {
            _iBandService = iBandService;

            if (GalaSoft.MvvmLight.ViewModelBase.IsInDesignModeStatic)
            {
                Quality = "Average";
                Rate = 80;
            }
        }

        private async void HeartRateOnReadingChanged(object sender, BandSensorReadingEventArgs<IBandHeartRateReading> e)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                 () =>
                 {
                     Quality = e.SensorReading.Quality.ToString();
                     Rate = e.SensorReading.HeartRate;
                 });
        }

        public async Task OnNavigatedTo()
        {
            _heartRate = await _iBandService.GetHeartRate();
            _heartRate.ReadingChanged += HeartRateOnReadingChanged;
            await _iBandService.StartReadingHeartRate();
        }

        public void OnNavigatedFrom()
        {
            _heartRate.ReadingChanged -= HeartRateOnReadingChanged;
            _iBandService.StopReadingHeartRate();
        }
    }
}