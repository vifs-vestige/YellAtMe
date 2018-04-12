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
using System.Windows.Shapes;

namespace YellAtMe
{
    /// <summary>
    /// Interaction logic for Random.xaml
    /// </summary>
    public partial class Random : Window
    {
        private AlarmTimer Alarm;
        private MainWindow Window;
        private bool Edit = false;
        private int ID;

        public Random(AlarmTimer alarm, MainWindow window)
        {
            InitializeComponent();
            Common(alarm, window);
            Time.Value = DateTime.Now;
        }

        public Random(AlarmTimer alarm, MainWindow window, DateTime time, int id)
        {
            InitializeComponent();
            Common(alarm, window);
            Time.Value = time;
            ID = id;
            Edit = true;
        }

        private void Common(AlarmTimer alarm, MainWindow window)
        {
            Alarm = alarm;
            Window = window;
            Show();
            window.Hide();
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            var time = (DateTime)Time.Value;
            if (Edit)
            {
                var temp = (RandomAlarm)Alarm.GetAlarm(ID);
                temp.SetTime(time.Year, time.Month, time.Day, time.Hour, time.Minute);
            }
            else
            {
                var temp = new RandomAlarm(time.Year, time.Month, time.Day, time.Hour, time.Minute);
                Alarm.AddAlarm(temp);
            }
            Window.AlarmGrid.Items.Refresh();
            Window.Show();
            Close();
        }


        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CloseRight(object sender, CancelEventArgs e)
        {
            Window.Show();
        }
    }
}
