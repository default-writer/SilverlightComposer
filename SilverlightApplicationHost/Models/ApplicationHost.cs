using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Presentation.Events;
using SilverlightApplicationHost.Interfaces;

namespace SilverlightApplicationHost.Models
{
    public class ApplicationHost : ViewModelBase, IApplicationHost
    {
        private readonly IEventAggregator _eventAggregator;
        private ObservableCollection<DataItem> _dataItems;
        private IDataItemsService _dataItemsService;

        /// <summary>
        ///     Imported constructor
        /// </summary>
        [ImportingConstructor]
        public ApplicationHost(IEventAggregator eventAggregator, IDataItemsService dataItemsService)
        {
            _eventAggregator = eventAggregator;
            _dataItemsService = dataItemsService;
        }

        /// <summary>
        ///     A sample collection
        /// </summary>
        public ObservableCollection<DataItem> dataItems
        {
            get { return _dataItems; }
            set
            {
                _dataItems = value;
                NotifyPropertyChanged("dataItems");
            }
        }

        #region IPartImportsSatisfiedNotification Members

        /// <summary>
        ///     Called when all the MEF imports are satistied
        /// </summary>
        public void OnImportsSatisfied()
        {
            //Initialize the dataItems
            dataItems = null;

            //Subscribe to the "DataItemsReceivedEvent"
            SubscriptionToken obj = _eventAggregator
                .GetEvent<CompositePresentationEvent<ObservableCollection<DataItem>>>().Subscribe(
                    dataItemsReceived => { dataItems = dataItemsReceived; },
                    true
                );

            //Call the Service
            _dataItemsService.GetDataItems();
        }

        #endregion

        /// <summary>
        ///     DataItems Service
        /// </summary>
        public IDataItemsService DataItemsService
        {
            get { return _dataItemsService; }
            set
            {
                _dataItemsService = value;
                NotifyPropertyChanged("dataItemsService");
            }
        }
    }
}