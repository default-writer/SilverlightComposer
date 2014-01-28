#region

using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Practices.Composite.Presentation.Commands;
using SilverlightApplicationHost.Interfaces;
using SilverlightApplicationHost.Interfaces.Services;

#endregion

namespace SilverlightApplicationHost.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public static DependencyProperty Panel1SourceProperty = DependencyProperty.Register("Panel1Source", typeof (string), typeof (MainPageViewModel), new PropertyMetadata(null, OnPanel1SourceProperty));

        public static DependencyProperty Panel2SourceProperty = DependencyProperty.Register("Panel2Source", typeof (string), typeof (MainPageViewModel), new PropertyMetadata(null, OnPanel2SourceProperty));

        private string _panel1Source;

        private string _panel2Source;

        public MainPageViewModel()
        {
            LoadPanel1Command = new DelegateCommand<Panel>(LoadPanel1);
            LoadPanel2Command = new DelegateCommand<Panel>(LoadPanel2);
            if (!DesignerProperties.IsInDesignTool)
            {
                CompositionInitializer.SatisfyImports(this);
            }
        }

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

        public string Panel2Source
        {
            get { return _panel2Source; }
            set
            {
                if (value != _panel2Source)
                {
                    _panel2Source = value;
                    NotifyPropertyChanged("Panel2Source");
                }
            }
        }

        private static void OnPanel1SourceProperty(object source, DependencyPropertyChangedEventArgs args)
        {
            var viewModel = (MainPageViewModel) source;
            viewModel.Panel1Source = (string) args.NewValue;
        }

        private static void OnPanel2SourceProperty(object source, DependencyPropertyChangedEventArgs args)
        {
            var viewModel = (MainPageViewModel) source;
            viewModel.Panel1Source = (string) args.NewValue;
        }

        private void Loaded(Panel element, string uri)
        {
            var strRevisedSelectedXapName = uri.Replace(".xap", ".");

            if (element.Children.Any(module => module.ToString().Contains(strRevisedSelectedXapName)))
            {
                return;
            }

            var selectedMefModule = MEFModuleList.ToList().FirstOrDefault(module => module.Value.ToString().Contains(strRevisedSelectedXapName));
            if (selectedMefModule != null)
            {
                element.Children.Add(selectedMefModule.Value);
            }
        }

        private void Unloaded(Panel element, string uri)
        {
            var strRevisedSelectedXapName = uri.Replace(".xap", ".");

            var selectedMefModuleInPanel = element.Children.FirstOrDefault(module => module.ToString().Contains(strRevisedSelectedXapName));

            if (selectedMefModuleInPanel != null)
            {
                element.Children.Remove(selectedMefModuleInPanel);
            }
        }

        #region Commanding

        public ICommand LoadPanel1Command { get; set; }

        public ICommand LoadPanel2Command { get; set; }

        public void LoadPanel1(Panel param)
        {
            CatalogService.Load(param, Panel1Source, Loaded, Unloaded);
        }

        public void LoadPanel2(Panel param)
        {
            CatalogService.Load(param, Panel2Source, Loaded, Unloaded);
        }

        #endregion

        #region Imports

        [Import(typeof (ICatalogService))]
        public ICatalogService CatalogService { get; set; }

        [ImportMany(AllowRecomposition = true)]
        public Lazy<UserControl>[] MEFModuleList { get; set; }

        #endregion
    }
}