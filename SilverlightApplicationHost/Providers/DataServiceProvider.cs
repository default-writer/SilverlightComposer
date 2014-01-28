#region

using System.ComponentModel.Composition;
using SilverlightApplicationHost.Interfaces.Services;
using SilverlightApplicationHost.Services;

#endregion

namespace SilverlightApplicationHost.Providers
{
    public class DataServiceProvider
    {
        private static readonly IDataService _dataService = new DataService();

        [Export(typeof (IDataService))]
        public IDataService DataService { get { return _dataService; } }
    }
}