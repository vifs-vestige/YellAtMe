using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace YellAtMe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        
        public MainWindow()
        {
            InitializeComponent();
            new AlarmTimer(this);
        }



        private delegate void UpdateTextCallback(string message); 

        public void SetTextBox(string s)
        {
            textBlock.Dispatcher.Invoke(
                new UpdateTextCallback(SetText),
                new object[] { s });
        }

        private void SetText(string s)
        {
            textBlock.Text = s;
        }

        #region contextMenu

        private void OpenWindow(object sender, RoutedEventArgs e)
        {
            Show();
        }

        private void CloseProgram(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        #endregion

        public void RedXHit(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}
