#region

using System;
using System.ComponentModel;

#endregion

namespace SilverlightApplicationHost.Interfaces
{
    public class ViewModelBase : IViewModelBase
    {
        /// <summary>
        ///     Event handler
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     NotifyPropertyChanged implementation
        /// </summary>
        public void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}