using System;
using AllInOneSensorDemo.UWP.ViewModels;

namespace AllInOneSensorDemo.UWP.Models
{
    public class MenuItem : ViewModelBase
    {
        private string _icon;
        private string _title;
        private Type _pageType;
        private string _displayName;

        public string Icon
        {
            get { return this._icon; }
            set { Set(ref this._icon, value); }
        }

        public string Title
        {
            get { return this._title; }
            set { Set(ref this._title, value); }
        }

        public Type PageType
        {
            get { return this._pageType; }
            set { Set(ref this._pageType, value); }
        }

        public string DisplayName
        {
            get { return this._displayName; }
            set { Set(ref this._displayName, value); }
        }

    }
}
