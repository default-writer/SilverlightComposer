#region

using System.ComponentModel;

#endregion

namespace SilverlightApplicationHost.Interfaces.Services
{
    public interface ITextService : INotifyPropertyChanged
    {
        string Text { get; set; }
    }
}