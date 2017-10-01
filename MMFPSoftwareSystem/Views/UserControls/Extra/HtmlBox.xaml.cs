using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MMFPSoftwareSystem.Views
{
    /// <summary>
    /// Interaction logic for HtmlBox.xaml
    /// </summary>
    public partial class HtmlBox : UserControl
    {
        public HtmlBox()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty HtmlAddressProperty = DependencyProperty.Register("HtmlAddress", typeof(string), typeof(HtmlBox));

        public Command ForwardCommand => _forwardCommand ?? (_forwardCommand = new Command(GoForward));
        public Command BackCommand => _backCommand ?? (_backCommand = new Command(GoBack));      

        private Command _forwardCommand;
        private Command _backCommand;
        
        public string HtmlAddress
        {
            get { return (string)GetValue(HtmlAddressProperty); }
            set { SetValue(HtmlAddressProperty, value); }
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.Property == HtmlAddressProperty)
            {
                DoBrowse();
            }
        }

        private void DoBrowse()
        {
            browser.Navigate(HtmlAddress);
        }

        private void GoForward()
        {
            if(browser.CanGoForward) browser.GoForward();
            Debug.Write("forward");
        }

        private void GoBack()
        {
            if (browser.CanGoBack) browser.GoBack();;
            Debug.Write("back");
        }
    }
}
