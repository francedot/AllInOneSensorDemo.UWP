﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using AllInOneSensorDemo.UWP.Services;
using AllInOneSensorDemo.UWP.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace AllInOneSensorDemo.UWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GeneralInfoPage : Page
    {
        public GeneralInfoPageViewModel GeneralInfoPageViewModel
           => ((ViewModelLocator)Application.Current.Resources["ViewModelLocator"]).GeneralInfoPageViewModel;

        public GeneralInfoPage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await GeneralInfoPageViewModel.OnNavigatedTo();
            base.OnNavigatedTo(e);
        }
    }
}
