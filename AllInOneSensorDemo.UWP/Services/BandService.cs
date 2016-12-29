using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AllInOneSensorDemo.UWP.ViewModels;
using Microsoft.Band;
using Microsoft.Band.Sensors;

namespace AllInOneSensorDemo.UWP.Services
{
    public class BandService : IBandService
    {
        private IBandInfo _selectedBand;
        private IBandClient _bandClient;

        public IBandSensor<IBandAccelerometerReading> Accelerometer { get; private set; }
        public IBandSensor<IBandGyroscopeReading> Gyroscope { get; private set; }
        public IBandSensor<IBandHeartRateReading> HeartRate { get; private set; }
        public IBandSensor<IBandCaloriesReading> Calories { get; private set; }
        public IBandSensor<IBandPedometerReading> Pedometer { get; private set; }
        public IBandSensor<IBandDistanceReading> Distance { get; private set; }
        public IBandSensor<IBandSkinTemperatureReading> SkinTemperature { get; private set; }
        public IBandSensor<IBandUVReading> UV { get; private set; }

        public bool IsConnected => _selectedBand != null;

        public async Task<bool> Init()
        {
            try
            {
                if (IsConnected) // TODO Client?
                    return true;

                var bands = await BandClientManager.Instance.GetBandsAsync();
                if (bands != null && bands.Length > 0)
                {
                    _selectedBand = bands[0]; // take the first band
                }

                if (_selectedBand != null)
                {
                    _bandClient = await BandClientManager.Instance.ConnectAsync(_selectedBand);

                    // connected!
                    await _bandClient.NotificationManager.VibrateAsync(Microsoft.Band.Notifications.VibrationType.NotificationAlarm);
                    return _bandClient != null;
                }

                return false;
            }
            catch (Exception x)
            {
                Debug.WriteLine(x.Message);
                return false;
            }
        }

        public string GetBandName()
        {
            return _selectedBand.Name;
        }

        public async Task<string> GetFirmwareVersion()
        {
            return await _bandClient.GetFirmwareVersionAsync();
        }

        public async Task<string> GetHardwareVersion()
        {
            return await _bandClient.GetHardwareVersionAsync();
        }

        public IBandSensor<IBandAccelerometerReading> GetAccelerometer()
        {
            if (IsConnected)
            {
                Accelerometer = _bandClient.SensorManager.Accelerometer;
                return Accelerometer;
            }
            return null;
        }

        public async Task StartReadingAccelerometer()
        {
            if (IsConnected)
            {
                _bandClient.SensorManager.Accelerometer.ReportingInterval = TimeSpan.FromMilliseconds(16.0);
                await _bandClient.SensorManager.Accelerometer.StartReadingsAsync();
            }
        }

        public async Task StopReadingAccelerometer()
        {
            await _bandClient.SensorManager.Accelerometer.StopReadingsAsync();
        }

        public IBandSensor<IBandGyroscopeReading> GetGyroscope()
        {
            if (IsConnected)
            {
                Gyroscope = _bandClient.SensorManager.Gyroscope;
                return Gyroscope;
            }
            return null;
        }

        public async Task StartReadingGyroscope()
        {
            if (IsConnected)
            {
                _bandClient.SensorManager.Gyroscope.ReportingInterval = TimeSpan.FromMilliseconds(16.0);
                await _bandClient.SensorManager.Gyroscope.StartReadingsAsync();
            }
        }

        public async Task StopReadingGyroscope()
        {
            await _bandClient.SensorManager.Gyroscope.StopReadingsAsync();
        }

        public async Task<IBandSensor<IBandHeartRateReading>> GetHeartRate()
        {
            if (IsConnected)
            {
                if (_bandClient.SensorManager.HeartRate.GetCurrentUserConsent() != UserConsent.Granted)
                {
                    // user hasn’t consented, request consent
                    await _bandClient.SensorManager.HeartRate.RequestUserConsentAsync();
                }

                // get a list of available reporting intervals
                // var supportedHeartBeatReportingIntervals = _bandClient.SensorManager.HeartRate.SupportedReportingIntervals;
                // foreach (var ri in supportedHeartBeatReportingIntervals)
                // {
                //     // do work with each reporting interval(i.e. , add them to a list in the UI)
                // }

                // // set the reporting interval
                // _bandClient.SensorManager.HeartRate.ReportingInterval =
                //supportedHeartBeatReportingIntervals.GetEnumerator().Current;

                HeartRate = _bandClient.SensorManager.HeartRate;
                return HeartRate;
            }
            return null;
        }

        public async Task StartReadingHeartRate()
        {
            if (IsConnected)
            {
                _bandClient.SensorManager.HeartRate.ReportingInterval = _bandClient.SensorManager.HeartRate.SupportedReportingIntervals.FirstOrDefault();
                await _bandClient.SensorManager.HeartRate.StartReadingsAsync();
            }
        }

        public async Task StopReadingHeartRate()
        {
            if (IsConnected)
            {
                await _bandClient.SensorManager.HeartRate.StopReadingsAsync();
            }
        }

        public IBandSensor<IBandCaloriesReading> GetCalories()
        {
            if (IsConnected)
            {
                Calories = _bandClient.SensorManager.Calories;
                return Calories;
            }
            return null;
        }

        public async Task StartReadingCalories()
        {
            if (IsConnected)
            {
                _bandClient.SensorManager.Calories.ReportingInterval = _bandClient.SensorManager.Calories.SupportedReportingIntervals.FirstOrDefault();
                await _bandClient.SensorManager.Calories.StartReadingsAsync();
            }
        }

        public async Task StopReadingCalories()
        {
            if (IsConnected)
            {
                await _bandClient.SensorManager.Calories.StopReadingsAsync();
            }
        }

        public IBandSensor<IBandPedometerReading> GetPedometer()
        {
            if (IsConnected)
            {
                Pedometer = _bandClient.SensorManager.Pedometer;
                return Pedometer;
            }
            return null;
        }

        public async Task StartReadingPedometer()
        {
            if (IsConnected)
            {
                _bandClient.SensorManager.Pedometer.ReportingInterval = _bandClient.SensorManager.Pedometer.SupportedReportingIntervals.FirstOrDefault();
                await _bandClient.SensorManager.Pedometer.StartReadingsAsync();
            }
        }

        public async Task StopReadingPedometer()
        {
            if (IsConnected)
            {
                await _bandClient.SensorManager.Pedometer.StopReadingsAsync();
            }
        }

        public IBandSensor<IBandDistanceReading> GetDistance()
        {
            if (IsConnected)
            {
                Distance = _bandClient.SensorManager.Distance;
                return Distance;
            }
            return null;
        }

        public async Task StartReadingDistance()
        {
            if (IsConnected)
            {
                _bandClient.SensorManager.Distance.ReportingInterval = _bandClient.SensorManager.Distance.SupportedReportingIntervals.FirstOrDefault();
                await _bandClient.SensorManager.Distance.StartReadingsAsync();
            }
        }

        public async Task StopReadingDistance()
        {
            if (IsConnected)
            {
                await _bandClient.SensorManager.Distance.StopReadingsAsync();
            }
        }

        public IBandSensor<IBandSkinTemperatureReading> GetSkinTemperature()
        {
            if (IsConnected)
            {
                SkinTemperature = _bandClient.SensorManager.SkinTemperature;
                return SkinTemperature;
            }
            return null;
        }

        public async Task StartReadingSkinTempearture()
        {
            if (IsConnected)
            {
                _bandClient.SensorManager.SkinTemperature.ReportingInterval = _bandClient.SensorManager.SkinTemperature.SupportedReportingIntervals.FirstOrDefault();
                await _bandClient.SensorManager.SkinTemperature.StartReadingsAsync();
            }
        }

        public async Task StopReadingSkinTempearture()
        {
            if (IsConnected)
            {
                await _bandClient.SensorManager.SkinTemperature.StopReadingsAsync();
            }
        }

        public IBandSensor<IBandUVReading> GetUV()
        {
            if (IsConnected)
            {
                UV = _bandClient.SensorManager.UV;
                return UV;
            }
            return null;
        }

        public async Task StartReadingUV()
        {
            if (IsConnected)
            {
                _bandClient.SensorManager.UV.ReportingInterval = _bandClient.SensorManager.UV.SupportedReportingIntervals.FirstOrDefault();
                await _bandClient.SensorManager.UV.StartReadingsAsync();
            }
        }

        public async Task StopReadingUV()
        {
            if (IsConnected)
            {
                await _bandClient.SensorManager.UV.StopReadingsAsync();
            }
        }
    }
}