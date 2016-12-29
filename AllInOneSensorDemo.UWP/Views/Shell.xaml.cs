using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using AllInOneSensorDemo.UWP.Models;
using AllInOneSensorDemo.UWP.ViewModels;

namespace AllInOneSensorDemo.UWP.Views
{
    public sealed partial class Shell : UserControl
    {
        public Shell()
        {
            this.InitializeComponent();

            var vm = new ShellViewModel();
            vm.PrimaryMenuItems.Add(new MenuItem { DisplayName = "General Info".ToUpper(), Icon = "\uE10F", Title = "General Info", PageType = typeof(GeneralInfoPage) });
            vm.PrimaryMenuItems.Add(new MenuItem { DisplayName = "Heart Rate".ToUpper(), Icon = "\uE00B", Title = "Heart Rate", PageType = typeof(HeartRatePage) });
            vm.PrimaryMenuItems.Add(new MenuItem { DisplayName = "Accelerometer".ToUpper(), Icon = "\uE7EC", Title = "Accelerometer", PageType = typeof(AccelerometerPage) });
            vm.PrimaryMenuItems.Add(new MenuItem { DisplayName = "Gyroscope".ToUpper(), Icon = "\uE7AD", Title = "Gyroscope", PageType = typeof(GyroscopePage) });
            vm.PrimaryMenuItems.Add(new MenuItem { DisplayName = "Pedometer".ToUpper(), Icon = "\uE805", Title = "Pedometer", PageType = typeof(PedometerPage) });
            vm.PrimaryMenuItems.Add(new MenuItem { DisplayName = "Distance".ToUpper(), Icon = "\uE709", Title = "Distance", PageType = typeof(DistancePage) });
            vm.PrimaryMenuItems.Add(new MenuItem { DisplayName = "Calories".ToUpper(), Icon = "\uE822", Title = "Calories", PageType = typeof(CaloriesPage) });
            vm.PrimaryMenuItems.Add(new MenuItem { DisplayName = "UV Exposure".ToUpper(), Icon = "\uE706", Title = "UV Exposure", PageType = typeof(UVPage) });
            vm.PrimaryMenuItems.Add(new MenuItem { DisplayName = "Skin Temperature".ToUpper(), Icon = "\uE781", Title = "Skin Temperature", PageType = typeof(TemperaturePage) });

            vm.SecondaryMenuItems.Add(new MenuItem { DisplayName = "More".ToUpper(), Icon = "\uE712", Title = "More", PageType = typeof(MorePage) });
            vm.SecondaryMenuItems.Add(new MenuItem { DisplayName = "Settings".ToUpper(), Icon = "\uE713", Title = "Settings", PageType = typeof(SettingsPage) });


            // select the first menu item
            vm.SelectedPrimaryMenuItem = vm.PrimaryMenuItems.First();

            this.ViewModel = vm;

            // add entry animations
            var transitions = new TransitionCollection { };
            var transition = new NavigationThemeTransition { };
            transitions.Add(transition);
            this.Frame.ContentTransitions = transitions;
        }

        public ShellViewModel ViewModel { get; private set; }

        public Frame RootFrame
        {
            get
            {
                return this.Frame;
            }
        }
    }
}
