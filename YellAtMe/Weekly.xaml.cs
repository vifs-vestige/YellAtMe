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

        public Weekly(AlarmTimer alarm, MainWindow window, List<DayOfWeek> days, DateTime time, int id)
        {
            InitializeComponent();
            ID = id;
            Edit = true;
            Time.Value = time;
            DayPicker.SelectedItemsOverride = days.Select(x => x.ToString()).ToList();
            Common(alarm, window);
        }

        private void Common(AlarmTimer alarm, MainWindow window)
        {
            Alarm = alarm;
            Window = window;
            DayPicker.ItemsSource = Enum.GetNames(typeof(DayOfWeek));
            Show();
            window.Hide();
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
                    temp.SetTime(days, time.Hour, time.Minute);
                }
                else
                {
                    var temp = new WeeklyAlarm(days, time.Hour, time.Minute);
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
            Window.Show();
        }
    }
}
