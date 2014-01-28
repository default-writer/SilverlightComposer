#region

using System.Collections.ObjectModel;
using SilverlightApplicationHost.Interfaces.Models;

#endregion

namespace SilverlightApplicationHost.Interfaces.Services
{
    public interface IDataService
    {
        ObservableCollection<IDataItem> DataItems { get; set; }
    }
}