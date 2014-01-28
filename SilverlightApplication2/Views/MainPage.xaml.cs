#region

using System.ComponentModel.Composition;
using System.Windows.Controls;

#endregion

namespace SilverlightApplication2.Views
{
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Export(typeof (UserControl))]
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }
    }
}