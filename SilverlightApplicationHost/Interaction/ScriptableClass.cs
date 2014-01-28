#region

using System.Windows;
using System.Windows.Browser;

#endregion

namespace SilverlightApplicationHost.Interaction
{
    public class ScriptableClass
    {
        [ScriptableMember]
        public void ShowAlertPopup(string message)
        {
            MessageBox.Show(message, "silverlight: ", MessageBoxButton.OK);
        }
    }
}