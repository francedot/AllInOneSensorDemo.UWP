using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using AllInOneSensorDemo.UWP.Services;
using GalaSoft.MvvmLight;
using Microsoft.Band.Sensors;

namespace AllInOneSensorDemo.UWP.ViewModels
{
    public class DistancePageViewModel : GalaSoft.MvvmLight.ViewModelBase
    {
        private readonly IBandService _iBandService;
        private IBandSensor<IBandDistanceReading> _distance;


        private string _motionType;
        public string MotionType
        {
            get { return _motionType; }
            set { Set(ref _motionType, value); }
        }

        private double _pace;
        public double Pace
        {
            get { return _pace; }
            set { Set(ref _pace, value); }
        }

        private double _speed;
        public double Speed
        {
            get { return _speed; }
            set { Set(ref _speed, value); }
        }

        private long _totalDistance;
        public long TotalDistance
        {
            get { return _totalDistance; }
            set { Set(ref _totalDistance, value); }
        }

        private long _todayDistance;
        public long TodayDistance
        {
            get { return _todayDistance; }
            set { Set(ref _todayDistance, value); }
        }

        public DistancePageViewModel(IBandService iBandService)
        {
            _iBandService = iBandService;

            if (GalaSoft.MvvmLight.ViewModelBase.IsInDesignModeStatic)
            {
                MotionType = "Walking";
                Pace = 0;
                Speed = 100;
                TotalDistance = 2000000;
                TodayDistance = 10000;
            }
        }

        private async void DistanceOnReadingChanged(object sender, BandSensorReadingEventArgs<IBandDistanceReading> e)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                 () =>
                 {
                     MotionType = e.SensorReading.CurrentMotion.ToString();
                     Pace = e.SensorReading.Pace;
                     Speed = e.SensorReading.Speed;
                     TotalDistance = e.SensorReading.TotalDistance;
                     TodayDistance = e.SensorReading.DistanceToday;
                 });
        }

        public async Task OnNavigatedTo()
        {
            _distance = _iBandService.GetDistance(); // TODO tutti cosi
            _distance.ReadingChanged += DistanceOnReadingChanged;
            await _iBandService.StartReadingDistance();
        }

        public void OnNavigatedFrom()
        {
            _distance.ReadingChanged -= DistanceOnReadingChanged;
            _iBandService.StopReadingDistance();
        }

    }
}