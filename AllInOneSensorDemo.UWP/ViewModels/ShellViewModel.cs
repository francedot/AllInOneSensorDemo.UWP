using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using AllInOneSensorDemo.UWP.Helpers;
using AllInOneSensorDemo.UWP.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace AllInOneSensorDemo.UWP.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        private readonly ObservableCollection<MenuItem> _primaryMenuItems = new ObservableCollection<MenuItem>();
        private readonly ObservableCollection<MenuItem> _secondaryMenuItems = new ObservableCollection<MenuItem>();
        private MenuItem _selectedPrimaryMenuItem;
        private MenuItem _selectedSecondaryMenuItem;
        private bool _isSplitViewPaneOpen;

        public ShellViewModel()
        {
            this.ToggleSplitViewPaneCommand = new Command(() => 
            this.IsSplitViewPaneOpen = !this.IsSplitViewPaneOpen);
        }

        public ICommand ToggleSplitViewPaneCommand { get; private set; }

        public bool IsSplitViewPaneOpen
        {
            get { return this._isSplitViewPaneOpen; }
            set { Set(ref this._isSplitViewPaneOpen, value); }
        }

        public MenuItem SelectedPrimaryMenuItem
        {
            get { return this._selectedPrimaryMenuItem; }
            set
            {
                if (Set(ref this._selectedPrimaryMenuItem, value))
                {
                    if (value == null)
                    {
                        return;
                    }
                    SelectedSecondaryMenuItem = null;
                    OnPropertyChanged("SelectedPageType");
                    // auto-close split view pane
                    this.IsSplitViewPaneOpen = false;
                }
            }
        }

        public MenuItem SelectedSecondaryMenuItem
        {
            get { return this._selectedSecondaryMenuItem; }
            set
            {
                if (Set(ref this._selectedSecondaryMenuItem, value))
                {
                    if (value == null)
                    {
                        return;
                    }
                    SelectedPrimaryMenuItem = null;
                    OnPropertyChanged("SelectedPageType");
                    // auto-close split view pane
                    this.IsSplitViewPaneOpen = false;
                }
            }
        }

        public Type SelectedPageType
        {
            get
            {
                if (this._selectedPrimaryMenuItem != null)
                {
                    return this._selectedPrimaryMenuItem.PageType;
                }
                else if (this._selectedSecondaryMenuItem != null)
                {
                    return this._selectedSecondaryMenuItem.PageType;
                }

                return null;
            }
            set
            {
                // select associated menu item
                var item = this._primaryMenuItems.FirstOrDefault(m => m.PageType == value);
                if (item != null)
                {
                    this.SelectedPrimaryMenuItem = item;
                    return;
                }
                
                item = this._secondaryMenuItems.FirstOrDefault(m => m.PageType == value);
                if (item != null)
                {
                    this.SelectedSecondaryMenuItem= item;
                }
            }
        }

        public ObservableCollection<MenuItem> PrimaryMenuItems
        {
            get { return this._primaryMenuItems; }
        }

        public ObservableCollection<MenuItem> SecondaryMenuItems
        {
            get { return this._secondaryMenuItems; }
        }
    }
}
