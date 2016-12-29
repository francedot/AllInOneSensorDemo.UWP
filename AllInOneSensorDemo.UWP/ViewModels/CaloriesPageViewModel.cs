using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.WebUI;
using AllInOneSensorDemo.UWP.Services;
using GalaSoft.MvvmLight;
using Microsoft.Band.Sensors;

namespace AllInOneSensorDemo.UWP.ViewModels
{
    public class CaloriesPageViewModel : GalaSoft.MvvmLight.ViewModelBase
    {
        private readonly IBandService _iBandService;

        private IBandSensor<IBandCaloriesReading> _calories;

        private long _totalCalories;
        public long TotalCalories
        {
            get { return _totalCalories; }
            set { Set(ref _totalCalories, value); }
        }

        private long _todayCalories;

        public long TodayCalories
        {
            get { return _todayCalories; }
            set { Set(ref _todayCalories, value); }
        }


        public CaloriesPageViewModel(IBandService iBandService)
        {
            _iBandService = iBandService;
            _calories = _iBandService.GetCalories();

            if (GalaSoft.MvvmLight.ViewModelBase.IsInDesignModeStatic)
            {
                TotalCalories = 100000;
                TodayCalories = 1000;
            }
        }

        private async void CaloriesOnReadingChanged(object sender, BandSensorReadingEventArgs<IBandCaloriesReading> e)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                 () =>
                 {
                     TotalCalories = e.SensorReading.Calories;
                     TodayCalories = e.SensorReading.CaloriesToday;
                 });
        }

        public async Task OnNavigatedTo()
        {
            _calories.ReadingChanged += CaloriesOnReadingChanged;
            await _iBandService.StartReadingCalories();
        }

        public async void OnNavigatedFrom()
        {
            _calories.ReadingChanged -= CaloriesOnReadingChanged;
            await _iBandService.StopReadingCalories();
        }
    }
}