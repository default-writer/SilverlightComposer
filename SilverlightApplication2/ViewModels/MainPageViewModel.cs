#region

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Presentation.Events;
using SilverlightApplication2.Commands;
using SilverlightApplicationHost.Interfaces;
using SilverlightApplicationHost.Interfaces.Commands;
using SilverlightApplicationHost.Interfaces.Models;
using SilverlightApplicationHost.Interfaces.Services;

#endregion

namespace SilverlightApplication2.ViewModels
{
    public class MainPageViewModel : ViewModelBase, IPartImportsSatisfiedNotification
    {
        public MainPageViewModel()
        {
            SaveCommand = new SaveCommand();
            if (!DesignerProperties.IsInDesignTool)
            {
                CompositionInitializer.SatisfyImports(this);
            }
        }

        #region SaveCommand

        public ISaveCommand SaveCommand { get; private set; }

        #endregion

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

        #region DataItemsService

        private IDataService _dataItemsService;

        [Import(typeof (IDataService))]
        public IDataService DataService
        {
            get { return _dataItemsService; }
            set
            {
                if (_dataItemsService != value)
                {
                    _dataItemsService = value;
                    NotifyPropertyChanged("DataItemsService");
                }
            }
        }

        #endregion

        #region Subscription

        private SubscriptionToken _obj;

        public void OnImportsSatisfied()
        {
            if (_obj == null)
            {
                _obj = _eventAggregator.GetEvent<CompositePresentationEvent<ObservableCollection<IDataItem>>>().Subscribe(dataItemsReceived => DataService.DataItems = dataItemsReceived, true);
            }
        }

        #endregion
    }
}