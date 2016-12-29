using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using AllInOneSensorDemo.UWP.Services;
using GalaSoft.MvvmLight;
using Microsoft.Band.Sensors;

namespace AllInOneSensorDemo.UWP.ViewModels
{
    public class UVPageViewModel : GalaSoft.MvvmLight.ViewModelBase
    {
        private readonly IBandService _iBandService;

        private IBandSensor<IBandUVReading> _uv;

        private string _indexLevel;
        public string IndexLevel
        {
            get { return _indexLevel; }
            set { Set(ref _indexLevel, value); }
        }

        private long _exposureToday;
        public long ExposureToday
        {
            get { return _exposureToday; }
            set { Set(ref _exposureToday, value); }
        }



        public UVPageViewModel(IBandService iBandService)
        {
            _iBandService = iBandService;
            if (GalaSoft.MvvmLight.ViewModelBase.IsInDesignModeStatic)
            {
                IndexLevel = "Low";
                ExposureToday = 10;
            }
        }

        private async void UVOnReadingChanged(object sender, BandSensorReadingEventArgs<IBandUVReading> e)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                 () =>
                 {
                     IndexLevel = e.SensorReading.IndexLevel.ToString();
                     ExposureToday = e.SensorReading.ExposureToday;
                 });
        }

        public async Task OnNavigatedTo()
        {
            _uv = _iBandService.GetUV(); // TODO tutti cosi
            _uv.ReadingChanged += UVOnReadingChanged;
            await _iBandService.StartReadingUV();
        }

        public void OnNavigatedFrom()
        {
            _uv.ReadingChanged -= UVOnReadingChanged;
            _iBandService.StopReadingUV();
        }
    }
}