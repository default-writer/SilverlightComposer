#region

using System.ComponentModel.Composition;
using Microsoft.Practices.Composite.Events;
using SilverlightApplicationHost.Interfaces.Services;

#endregion

namespace SilverlightApplicationHost.Providers
{
    public sealed class EventAggregatorServiceProvider
    {
        internal readonly IEventAggregator _eventAggregator = new EventAggregator();

        [Export(typeof (IEventAggregatorService))]
        public IEventAggregator EventAggregator { get { return _eventAggregator; } }
    }
}