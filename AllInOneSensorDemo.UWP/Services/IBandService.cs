using System;
using System.Threading.Tasks;
using Microsoft.Band.Sensors;

namespace AllInOneSensorDemo.UWP.Services
{
    public interface IBandService
    {
        Task<bool> Init();

        string GetBandName();
        Task<string> GetFirmwareVersion();
        Task<string> GetHardwareVersion();

        IBandSensor<IBandAccelerometerReading> GetAccelerometer();
        Task StartReadingAccelerometer();
        Task StopReadingAccelerometer();

        IBandSensor<IBandGyroscopeReading> GetGyroscope();
        Task StartReadingGyroscope();
        Task StopReadingGyroscope();

        Task<IBandSensor<IBandHeartRateReading>> GetHeartRate();
        Task StartReadingHeartRate();
        Task StopReadingHeartRate();

        IBandSensor<IBandCaloriesReading> GetCalories();
        Task StartReadingCalories();
        Task StopReadingCalories();

        IBandSensor<IBandPedometerReading> GetPedometer();
        Task StartReadingPedometer();
        Task StopReadingPedometer();

        IBandSensor<IBandDistanceReading> GetDistance();
        Task StartReadingDistance();
        Task StopReadingDistance();

        IBandSensor<IBandSkinTemperatureReading> GetSkinTemperature();
        Task StartReadingSkinTempearture();
        Task StopReadingSkinTempearture();

        IBandSensor<IBandUVReading> GetUV();
        Task StartReadingUV();
        Task StopReadingUV();

    }
}