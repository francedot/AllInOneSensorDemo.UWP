using AllInOneSensorDemo.UWP.ViewModels;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace AllInOneSensorDemo.UWP.Services
{
    public class ViewModelLocator
    {
        public static readonly string ViewsAssemblyName = "AllInOneSensorDemo.UWP.Views.";

        public RootPageViewModel RootPageViewModel =>
            ServiceLocator.Current.GetInstance<RootPageViewModel>();

        public GeneralInfoPageViewModel GeneralInfoPageViewModel =>
            ServiceLocator.Current.GetInstance<GeneralInfoPageViewModel>();

        public AccelerometerPageViewModel AccelerometerPageViewModel =>
            ServiceLocator.Current.GetInstance<AccelerometerPageViewModel>();

        public CaloriesPageViewModel CaloriesPageViewModel =>
            ServiceLocator.Current.GetInstance<CaloriesPageViewModel>();

        public GyroscopePageViewModel GyroscopePageViewModel =>
        ServiceLocator.Current.GetInstance<GyroscopePageViewModel>();

        public HeartRatePageViewModel HeartRatePageViewModel =>
            ServiceLocator.Current.GetInstance<HeartRatePageViewModel>();

        public MainPageViewModel MainPageViewModel =>
            ServiceLocator.Current.GetInstance<MainPageViewModel>();

        public PedometerPageViewModel PedometerPageViewModel =>
            ServiceLocator.Current.GetInstance<PedometerPageViewModel>();

        public DistancePageViewModel DistancePageViewModel =>
            ServiceLocator.Current.GetInstance<DistancePageViewModel>();

        public SkinTemperaturePageViewModel TemperaturePageViewModel =>
            ServiceLocator.Current.GetInstance<SkinTemperaturePageViewModel>();

        public UVPageViewModel UVPageViewModel =>
            ServiceLocator.Current.GetInstance<UVPageViewModel>();

        public SettingsPageViewModel SettingsPageViewModel =>
            ServiceLocator.Current.GetInstance<SettingsPageViewModel>();

        public MorePageViewModel MorePageViewModel =>
            ServiceLocator.Current.GetInstance<MorePageViewModel>();

        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (GalaSoft.MvvmLight.ViewModelBase.IsInDesignModeStatic)
            {
                // We are at design time.
                SimpleIoc.Default.Register<RootPageViewModel>();
                SimpleIoc.Default.Register<GeneralInfoPageViewModel>();
                SimpleIoc.Default.Register<AccelerometerPageViewModel>();
                SimpleIoc.Default.Register<CaloriesPageViewModel>();
                SimpleIoc.Default.Register<GyroscopePageViewModel>();
                SimpleIoc.Default.Register<HeartRatePageViewModel>();
                SimpleIoc.Default.Register<MainPageViewModel>();
                SimpleIoc.Default.Register<PedometerPageViewModel>();
                SimpleIoc.Default.Register<DistancePageViewModel>();
                SimpleIoc.Default.Register<SkinTemperaturePageViewModel>();
                SimpleIoc.Default.Register<UVPageViewModel>();
                SimpleIoc.Default.Register<SettingsPageViewModel>();
                SimpleIoc.Default.Register<MorePageViewModel>();
            }
            else
            {
                // We are at runtime.
                SimpleIoc.Default.Register<RootPageViewModel>();
                SimpleIoc.Default.Register<GeneralInfoPageViewModel>();
                SimpleIoc.Default.Register<AccelerometerPageViewModel>();
                SimpleIoc.Default.Register<CaloriesPageViewModel>();
                SimpleIoc.Default.Register<GyroscopePageViewModel>();
                SimpleIoc.Default.Register<HeartRatePageViewModel>();
                SimpleIoc.Default.Register<MainPageViewModel>();
                SimpleIoc.Default.Register<PedometerPageViewModel>();
                SimpleIoc.Default.Register<DistancePageViewModel>();
                SimpleIoc.Default.Register<SkinTemperaturePageViewModel>();
                SimpleIoc.Default.Register<UVPageViewModel>();
                SimpleIoc.Default.Register<SettingsPageViewModel>();
                SimpleIoc.Default.Register<MorePageViewModel>();
            }
        }
    }
}