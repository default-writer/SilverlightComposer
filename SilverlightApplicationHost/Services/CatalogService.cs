#region

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Windows.Controls;
using SilverlightApplicationHost.Interfaces.Services;

#endregion

namespace SilverlightApplicationHost.Services
{
    [Export(typeof (ICatalogService))]
    public class CatalogService : ICatalogService
    {
        private static AggregateCatalog _aggregateCatalog;
        private readonly Dictionary<string, Action<Panel, string>> _actions;
        private readonly Dictionary<string, DeploymentCatalog> _catalogs;
        private readonly Dictionary<string, Panel> _panels;
        private readonly Dictionary<string, Action<Panel, string>> _undoActions;

        public CatalogService()
        {
            _catalogs = new Dictionary<string, DeploymentCatalog>();
            _actions = new Dictionary<string, Action<Panel, string>>();
            _undoActions = new Dictionary<string, Action<Panel, string>>();
            _panels = new Dictionary<string, Panel>();
        }

        public void Load(Panel element, string uri, Action<Panel, string> action, Action<Panel, string> undoAction)
        {
            if (!string.IsNullOrEmpty(uri))
            {
                if (action != null)
                {
                    _actions[uri] = action;
                }
                if (undoAction != null)
                {
                    _undoActions[uri] = undoAction;
                }
                if (element != null)
                {
                    _panels[uri] = element;
                }
                if (_catalogs.ContainsKey(uri))
                {
                    _actions[uri](_panels[uri], uri);
                    return;
                }

                var catalog = new DeploymentCatalog(uri);
                catalog.DownloadCompleted += (s, e) =>
                {
                    if (e.Error != null)
                    {
                        throw e.Error;
                    }
                    _actions[uri](_panels[uri], uri);
                };
                catalog.DownloadAsync();
                _catalogs[uri] = catalog;
                _aggregateCatalog.Catalogs.Add(catalog);
            }
        }

        public void Unload(string uri)
        {
            // Remove a .xap from the catalog
            DeploymentCatalog catalog;
            if (_catalogs.TryGetValue(uri, out catalog))
            {
                _undoActions[uri](_panels[uri], uri);
                _aggregateCatalog.Catalogs.Remove(catalog);
                _catalogs.Remove(uri);
            }
        }

        public void Update(Panel element, string uri, Action<Panel, string> action)
        {
            if (!string.IsNullOrEmpty(uri))
            {
                if (!_catalogs.ContainsKey(uri))
                {
                    return;
                }
                DeploymentCatalog catalog;
                _catalogs.TryGetValue(uri, out catalog);
                action(element, uri);
                _catalogs[uri] = catalog;
            }
        }

        public static void Initialize()
        {
            _aggregateCatalog = new AggregateCatalog();
            _aggregateCatalog.Catalogs.Add(new DeploymentCatalog());
            CompositionHost.Initialize(_aggregateCatalog);
        }
    }
}