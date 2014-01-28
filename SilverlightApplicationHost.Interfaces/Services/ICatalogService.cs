#region

using System;
using System.Windows.Controls;

#endregion

namespace SilverlightApplicationHost.Interfaces.Services
{
    public interface ICatalogService
    {
        void Load(Panel panel, string uri, Action<Panel, string> action, Action<Panel, string> undoAction);
        void Unload(string uri);
    }
}