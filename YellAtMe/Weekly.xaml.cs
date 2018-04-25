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
    /// Interaction logic for Weekly.xaml
    /// </summary>
    public partial class Weekly : Window
    {
        private AlarmTimer Alarm;
        private MainWindow Window;
        private bool Edit = false;
        private int ID;
        public Weekly(AlarmTimer alarm, MainWindow window)
        {
            InitializeComponent();
            Common(alarm, window);
            Time.Value = DateTime.Now;
        }

        public Weekly(AlarmTimer alarm, MainWindow window, WeeklyAlarm weeklyAlarm)
        {
            InitializeComponent();
            ID = weeklyAlarm.ID;
            Edit = true;
            Time.Value = weeklyAlarm.GetAlarm();
            DayPicker.SelectedItemsOverride = weeklyAlarm.GetDays().Select(x => x.ToString()).ToList();
            AlarmText.Text = weeklyAlarm.AlarmText;
            AlarmSoundFile.Text = weeklyAlarm.GetAlarmSound();
            Common(alarm, window);
        }

        private void Common(AlarmTimer alarm, MainWindow window)
        {
            Alarm = alarm;
            Window = window;
            DayPicker.ItemsSource = Enum.GetNames(typeof(DayOfWeek));
            Show();
            window.Hide();
            window.DisallowOpenWindow();
        }

        private void PickFile(object sender, RoutedEventArgs e)
        {
            AlarmSoundFile.Text = AlarmWindowTools.PickFile();
        }

        private void RemoveFile(object sender, RoutedEventArgs e)
        {
            AlarmSoundFile.Text = "";
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            var selectedDays = DayPicker.SelectedItems;
            if (selectedDays.Count == 0)
            {
                MessageBox.Show("You need days selected");
            }
            else
            {
                var time = (DateTime)Time.Value;
                var days = new List<DayOfWeek>();
                foreach (var item in selectedDays)
                {
                    var day = (DayOfWeek)System.Enum.Parse(typeof(DayOfWeek), item.ToString());
                    days.Add(day);
                }
                if (Edit)
                {
                    var temp = (WeeklyAlarm)Alarm.GetAlarm(ID);
                    temp.SetAlarmSound(AlarmSoundFile.Text);
                    temp.AlarmText = AlarmText.Text;
                    temp.SetTime(days, time.Hour, time.Minute);
                }
                else
                {
                    var temp = new WeeklyAlarm(days, time.Hour, time.Minute);
                    temp.SetAlarmSound(AlarmSoundFile.Text);
                    temp.AlarmText = AlarmText.Text;
                    Alarm.AddAlarm(temp);
                }
                Window.AlarmGrid.Items.Refresh();
                Window.Show();
                Close();
            }
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CloseRight(object sender, CancelEventArgs e)
        {
            Window.AllowOpenWindow();
            try
            {
                Window.Show();
            }
            catch (Exception) { }
        }
    }
}
