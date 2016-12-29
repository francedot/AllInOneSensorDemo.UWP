using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using AllInOneSensorDemo.UWP.Services;
using GalaSoft.MvvmLight;
using Microsoft.Band.Sensors;

namespace AllInOneSensorDemo.UWP.ViewModels
{
    public class GyroscopePageViewModel : GalaSoft.MvvmLight.ViewModelBase
    {
        private readonly IBandService _iBandService;
        private readonly IBandSensor<IBandGyroscopeReading> _gyroscope;


        private double _angularVelocityXAxis;
        public double AngularVelocityXAxis
        {
            get { return _angularVelocityXAxis; }
            set { Set(ref _angularVelocityXAxis, value); }
        }

        private double _angularVelocityYAxis;
        public double AngularVelocityYAxis
        {
            get { return _angularVelocityYAxis; }
            set { Set(ref _angularVelocityYAxis, value); }
        }

        private double _angularVelocityZAxis;

        public double AngularVelocityZAxis
        {
            get { return _angularVelocityZAxis; }
            set { Set(ref _angularVelocityZAxis, value); }
        }

        private double _angularAccelerationXAxis;
        public double AngularAccelerationXAxis
        {
            get { return _angularAccelerationXAxis; }
            set { Set(ref _angularAccelerationXAxis, value); }
        }

        private double _angularAccelerationYAxis;
        public double AngularAccelerationYAxis
        {
            get { return _angularAccelerationYAxis; }
            set { Set(ref _angularAccelerationYAxis, value); }
        }

        private double _angularAccelerationZAxis;

        public double AngularAccelerationZAxis
        {
            get { return _angularAccelerationZAxis; }
            set { Set(ref _angularAccelerationZAxis, value); }
        }

        public GyroscopePageViewModel(IBandService iBandService)
        {
            _iBandService = iBandService;
            _gyroscope = _iBandService.GetGyroscope();

            if (GalaSoft.MvvmLight.ViewModelBase.IsInDesignModeStatic)
            {
                AngularVelocityXAxis = 4.2;
                AngularVelocityYAxis = 6.7;
                AngularVelocityZAxis = 9.2;

                AngularAccelerationXAxis = 4.2;
                AngularAccelerationYAxis = 6.7;
                AngularAccelerationZAxis = 9.2;
            }
        }

        private async void GyroscopeOnReadingChanged(object sender, BandSensorReadingEventArgs<IBandGyroscopeReading> e)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                 () =>
                 {
                     AngularVelocityXAxis = e.SensorReading.AngularVelocityX;
                     AngularVelocityYAxis = e.SensorReading.AngularVelocityY;
                     AngularVelocityZAxis = e.SensorReading.AngularVelocityZ;

                     AngularAccelerationXAxis = e.SensorReading.AccelerationX;
                     AngularAccelerationYAxis = e.SensorReading.AccelerationY;
                     AngularAccelerationZAxis = e.SensorReading.AccelerationZ;
                 });
        }

        public async Task OnNavigatedTo()
        {
            _gyroscope.ReadingChanged += GyroscopeOnReadingChanged;
            await _iBandService.StartReadingGyroscope();
        }

        public void OnNavigatedFrom()
        {
            _gyroscope.ReadingChanged -= GyroscopeOnReadingChanged;
            _iBandService.StopReadingGyroscope();
        }
    }
}