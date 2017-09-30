using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MMFPSoftwareSystem.Views
{
    /// <summary>
    /// Interaction logic for AdminView.xaml
    /// </summary>
    public partial class AdminView : Window
    {
        public AdminView()
        {
            InitializeComponent();
            this.Loaded += OnLoaded;
            this.Closed += OnClosed;
        }

        private void OnLoaded(object sender, EventArgs eventArgs)
        {
            this.Owner.IsEnabled = false;
        }

        private void OnClosed(object sender, EventArgs e)
        {
            this.Owner.IsEnabled = true;
        }
    }
}
