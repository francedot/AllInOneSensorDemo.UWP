using System;
using Windows.UI.Xaml;
using AllInOneSensorDemo.UWP.Helpers;
using AllInOneSensorDemo.UWP.Services;
using AllInOneSensorDemo.UWP.Settings;

namespace AllInOneSensorDemo.UWP.ViewModels
{
    public class SettingsPageViewModel : ViewModelBase
    {
        private readonly SettingsService _settings;

        public SettingsPageViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                // designtime
            }
            else
            {
                _settings = SettingsService.Instance;
            }
        }

        public bool UseLightThemeButton
        {
            get { return _settings.AppTheme.Equals(ApplicationTheme.Light); }
            set
            {
                _settings.AppTheme = value ? ApplicationTheme.Light : ApplicationTheme.Dark;
                OnPropertyChanged();
            }
        }
        
        /// <summary>
        /// Inline SettingsService Class
        /// </summary>
        public class SettingsService
        {
            public static SettingsService Instance { get; } = new SettingsService();
            readonly ISettingsHelper _helper;
            private SettingsService()
            {
                _helper = new SettingsHelper();
            }

            public ApplicationTheme AppTheme
            {
                get
                {
                    var theme = ApplicationTheme.Light;
                    var value = _helper.Read<string>(nameof(AppTheme), theme.ToString());
                    return Enum.TryParse<ApplicationTheme>(value, out theme) ? theme : ApplicationTheme.Dark;
                }
                set
                {
                    _helper.Write(nameof(AppTheme), value.ToString());
                    ((FrameworkElement)Window.Current.Content).RequestedTheme = value.ToElementTheme();
                }
            }

            public TimeSpan CacheMaxDuration
            {
                get { return _helper.Read<TimeSpan>(nameof(CacheMaxDuration), TimeSpan.FromDays(2)); }
                set
                {
                    _helper.Write(nameof(CacheMaxDuration), value);
                }
            }
        }
    }

}
