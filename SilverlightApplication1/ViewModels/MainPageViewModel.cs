#region

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Presentation.Commands;
using Microsoft.Practices.Composite.Presentation.Events;
using SilverlightApplicationHost.Interfaces;
using SilverlightApplicationHost.Interfaces.Commands;
using SilverlightApplicationHost.Interfaces.Models;
using SilverlightApplicationHost.Interfaces.Services;

#endregion

namespace SilverlightApplication1.ViewModels
{
    public class MainPageViewModel : ViewModelBase, IPartImportsSatisfiedNotification
    {
        public MainPageViewModel()
        {
            UpdatePanel1Command = new DelegateCommand<Panel>(UpdatePanel1);
            if (!DesignerProperties.IsInDesignTool)
            {
                CompositionInitializer.SatisfyImports(this);
            }
        }

        #region Panel1Source

        public static DependencyProperty Panel1SourceProperty = DependencyProperty.Register("Panel1Source", typeof (string), typeof (MainPageViewModel), new PropertyMetadata(null, OnPanel1SourceProperty));

        private string _panel1Source;

        public string Panel1Source
        {
            get { return _panel1Source; }
            set
            {
                if (value != _panel1Source)
                {
                    _panel1Source = value;
                    NotifyPropertyChanged("Panel1Source");
                }
            }
        }

        private static void OnPanel1SourceProperty(object source, DependencyPropertyChangedEventArgs args)
        {
            var viewModel = (MainPageViewModel) source;
            viewModel.Panel1Source = (string) args.NewValue;
        }

        #endregion

        #region LoadCommand

        private ILoadCommand _updateCommand;

        [Import(typeof (ILoadCommand))]
        public ILoadCommand LoadCommand
        {
            get { return _updateCommand; }
            set
            {
                if (_updateCommand != value)
                {
                    _updateCommand = value;
                    NotifyPropertyChanged("LoadCommand");
                }
            }
        }

        #endregion

        #region LoadCommand

        public DelegateCommand<Panel> UpdatePanel1Command { get; set; }

        public void UpdatePanel1(Panel param)
        {
            CatalogService.Unload(Panel1Source);
            CatalogService.Load(param, Panel1Source, null, null);
        }

        #endregion

        #region AddCommand

        private IAddCommand _addCommand;

        [Import(typeof (IAddCommand))]
        public IAddCommand AddCommand
        {
            get { return _addCommand; }
            set
            {
                if (_addCommand != value)
                {
                    _addCommand = value;
                    NotifyPropertyChanged("AddCommand");
                }
            }
        }

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

        #region DataService

        private IDataService _dataService;

        [Import(typeof (IDataService))]
        public IDataService DataService
        {
            get { return _dataService; }
            set
            {
                if (_dataService != value)
                {
                    _dataService = value;
                    NotifyPropertyChanged("DataService");
                }
            }
        }

        #endregion

        #region CataloService

        private ICatalogService _catalogService;

        [Import(typeof (ICatalogService))]
        public ICatalogService CatalogService
        {
            get { return _catalogService; }
            set
            {
                if (_catalogService != value)
                {
                    _catalogService = value;
                    NotifyPropertyChanged("CatalogService");
                }
            }
        }

        #endregion

        #region TextService

        private ITextService _text;

        [Import(typeof (ITextService))]
        public ITextService TextService
        {
            get { return _text; }
            set
            {
                if (_text != value)
                {
                    _text = value;
                    NotifyPropertyChanged("TextService");
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