#region

using System.ComponentModel.Composition;
using SilverlightApplicationHost.Interfaces;
using SilverlightApplicationHost.Interfaces.Services;

#endregion

namespace SilverlightApplicationHost.Services
{
    [Export(typeof (ITextService))]
    public class TextService : ViewModelBase, ITextService
    {
        private string _dataItemText;

        public string Text
        {
            get { return _dataItemText; }
            set
            {
                if (_dataItemText != value)
                {
                    _dataItemText = value;
                    NotifyPropertyChanged("Text");
                }
            }
        }
    }
}