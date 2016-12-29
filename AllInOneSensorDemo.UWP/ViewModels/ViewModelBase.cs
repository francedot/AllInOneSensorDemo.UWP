﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AllInOneSensorDemo.UWP.ViewModels
{
    /// <summary>
    /// The VM guidelines suggest that the VM should expose a property for each value on the model that it wants to expose.
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        protected bool Set<T>(ref T field, T value,
            [CallerMemberName] string name = null)
        {
            if (Equals(field, value))
                return false;
            field = value;
            OnPropertyChanged(name);
            return true;
        }

        #endregion
    }
}