#region

using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Presentation.Events;
using SilverlightApplicationHost.Interfaces;
using SilverlightApplicationHost.Interfaces.Commands;
using SilverlightApplicationHost.Interfaces.Models;
using SilverlightApplicationHost.Interfaces.Services;

#endregion

namespace SilverlightApplicationHost.Commands
{
    [Export(typeof (ILoadCommand))]
    public class LoadCommand : ViewModelBase, ILoadCommand
    {
        #region EventAggregator

        private IEventAggregator _eventAggregator;

        [Import(typeof (IEventAggregatorService))]
        public IEventAggregator EventAggregator
        {
            get { return _eventAggregator; }
            set
            {
                if (_eventAggregator != value)
                {
                    _eventAggregator = value;
                    NotifyPropertyChanged("EventAggregator");
                }
            }
        }

        #endregion

        #region Command

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        /// <summary>
        ///     Execute the command. Add a DataItem to the collection
        /// </summary>
        public void Execute(object parameter)
        {
            var collection = (parameter as ObservableCollection<IDataItem>) ?? new ObservableCollection<IDataItem>();
            _eventAggregator.GetEvent<CompositePresentationEvent<ObservableCollection<IDataItem>>>().Publish(collection);
        }

        #endregion
    }
}