#region

using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Presentation.Events;
using SilverlightApplicationHost.Interfaces;
using SilverlightApplicationHost.Interfaces.Models;
using SilverlightApplicationHost.Interfaces.Services;

#endregion

namespace SilverlightApplicationHost.Services
{
    public class DataService : ViewModelBase, IDataService
    {
        private ObservableCollection<IDataItem> _dataItems;
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

        public ObservableCollection<IDataItem> DataItems
        {
            get { return _dataItems; }
            set
            {
                if (_dataItems != value)
                {
                    _dataItems = value;
                    NotifyPropertyChanged("DataItems");
                }
            }
        }

        public void Publish()
        {
            _eventAggregator.GetEvent<CompositePresentationEvent<ObservableCollection<IDataItem>>>().Publish(_dataItems);
        }
    }
}