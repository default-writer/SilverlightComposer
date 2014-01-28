#region

using System.ComponentModel.Composition;
using System.Windows.Controls;

#endregion

namespace SilverlightApplication1.Views
{
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Export(typeof (UserControl))]
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
    }
}