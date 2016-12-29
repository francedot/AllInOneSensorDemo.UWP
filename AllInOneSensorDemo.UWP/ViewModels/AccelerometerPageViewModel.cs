using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using AllInOneSensorDemo.UWP.Services;
using Microsoft.Band.Sensors;

namespace AllInOneSensorDemo.UWP.ViewModels
{
    public class AccelerometerPageViewModel : GalaSoft.MvvmLight.ViewModelBase
    {
        private readonly IBandService _iBandService;

        private IBandSensor<IBandAccelerometerReading> _accelerometer;

        private double _xAxis;
        public double XAxis
        {
            get { return _xAxis; }
            set { Set(ref _xAxis, value); }
        }

        private double _yAxis;
        public double YAxis
        {
            get { return _yAxis; }
            set { Set(ref _yAxis, value); }
        }

        private double _zAxis;

        public double ZAxis
        {
            get { return _zAxis; }
            set { Set(ref _zAxis, value); }
        }

        public AccelerometerPageViewModel(IBandService iBandService)
        {
            _iBandService = iBandService;
            _accelerometer = _iBandService.GetAccelerometer();

            if (GalaSoft.MvvmLight.ViewModelBase.IsInDesignModeStatic)
            {
                XAxis = 4.2;
                YAxis = 6.7;
                ZAxis = 9.2;
            }
        }

        private async void AccelerometerOnReadingChanged(object sender, BandSensorReadingEventArgs<IBandAccelerometerReading> e)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                 () =>
                 {
                     XAxis = e.SensorReading.AccelerationX;
                     YAxis = e.SensorReading.AccelerationY;
                     ZAxis = e.SensorReading.AccelerationZ;
                 });
        }

        public async Task OnNavigatedTo()
        {
            _accelerometer.ReadingChanged += AccelerometerOnReadingChanged;
            await _iBandService.StartReadingAccelerometer();
        }

        public void OnNavigatedFrom()
        {
            _accelerometer.ReadingChanged -= AccelerometerOnReadingChanged;
            _iBandService.StopReadingAccelerometer();
        }
    }
}