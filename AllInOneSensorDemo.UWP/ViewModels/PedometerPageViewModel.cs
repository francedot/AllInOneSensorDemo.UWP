using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using AllInOneSensorDemo.UWP.Services;
using GalaSoft.MvvmLight;
using Microsoft.Band.Sensors;

namespace AllInOneSensorDemo.UWP.ViewModels
{
    public class PedometerPageViewModel : GalaSoft.MvvmLight.ViewModelBase
    {
        private readonly IBandService _iBandService;
        private IBandSensor<IBandPedometerReading> _pedometer;

        private long _totalSteps;
        public long TotalSteps
        {
            get { return _totalSteps; }
            set { Set(ref _totalSteps, value); }
        }


        private long _todaySteps;
        public long TodaySteps
        {
            get { return _todaySteps; }
            set { Set(ref _todaySteps, value); }
        }

        public PedometerPageViewModel(IBandService iBandService)
        {
            _iBandService = iBandService;
            _pedometer = _iBandService.GetPedometer();

            if (GalaSoft.MvvmLight.ViewModelBase.IsInDesignModeStatic)
            {
                TotalSteps = 1000000;
                TodaySteps = 4000;
            }
        }

        private async void PedometerOnReadingChanged(object sender, BandSensorReadingEventArgs<IBandPedometerReading> e)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                 () =>
                 {
                     TotalSteps = e.SensorReading.TotalSteps;
                     TodaySteps = e.SensorReading.StepsToday;
                 });
        }

        public async Task OnNavigatedTo()
        {
            _pedometer = _iBandService.GetPedometer(); // TODO tutti cosi
            _pedometer.ReadingChanged += PedometerOnReadingChanged;
            await _iBandService.StartReadingPedometer();
        }

        public void OnNavigatedFrom()
        {
            _pedometer.ReadingChanged -= PedometerOnReadingChanged;
            _iBandService.StopReadingPedometer();
        }

    }
}