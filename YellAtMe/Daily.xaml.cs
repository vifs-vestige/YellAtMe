using Microsoft.Win32;
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
using Xceed.Wpf.Toolkit;

namespace YellAtMe
{
    /// <summary>
    /// Interaction logic for Daily.xaml
    /// </summary>
    public partial class Daily : Window
    {
        private AlarmTimer Alarm;
        private MainWindow Window;
        private bool Edit = false;
        private int ID;

        public Daily(AlarmTimer alarm, MainWindow window)
        {
            InitializeComponent();
            Common(alarm, window);
            Time.Value = DateTime.Now;
        }

        public Daily(AlarmTimer alarm, MainWindow window, DailyAlarm dailyAlarm)
        {
            InitializeComponent();
            Common(alarm, window);
            Edit = true;
            ID = dailyAlarm.ID;
            Time.Value = dailyAlarm.GetAlarm();
            AlarmText.Text = dailyAlarm.AlarmText;
            AlarmSoundFile.Text = dailyAlarm.GetAlarmSound();
        }

        private void Common(AlarmTimer alarm, MainWindow window)
        {
            Alarm = alarm;
            Window = window;
            Show();
            window.Hide();
            window.DisallowOpenWindow();         
        }

        private void PickFile(object sender, RoutedEventArgs e)
        {
            AlarmSoundFile.Text = AlarmWindowTools.PickFile();
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            var time = (DateTime)Time.Value;
            if (Edit)
            {
                var temp = (DailyAlarm)Alarm.GetAlarm(ID);
                temp.SetAlarmSound(AlarmSoundFile.Text);
                temp.AlarmText = AlarmText.Text;
                temp.SetTime(time.Hour, time.Minute);
            }
            else
            {
                var temp = new DailyAlarm(time.Hour, time.Minute);
                temp.SetAlarmSound(AlarmSoundFile.Text);
                temp.AlarmText = AlarmText.Text;
                Alarm.AddAlarm(temp);
            }
            Window.AlarmGrid.Items.Refresh();
            Close();
        }


        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CloseRight(object sender, CancelEventArgs e)
        {
            Window.AllowOpenWindow();
            //Empty Catch if you use context menu to close program while this open
            //I know bad, but whatever, easier then doing some logic and method calling to
            //make sure window.open isn't called if close is called while this open
            try
            {
                Window.Show();
            }
            catch (Exception) { }
        }

    }
}
