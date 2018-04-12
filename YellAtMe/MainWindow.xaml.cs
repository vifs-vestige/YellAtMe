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

    //public class Test
    //{
    //    public string AlarmType { get; set; }
    //    public string AlarmTime { get; set; }

    //    public int ID { get; set; }
    //}
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AlarmTimer Alarm;

        
        public MainWindow()
        {
            InitializeComponent();
            Alarm = new AlarmTimer(this);
            //List<Test> temp = new List<Test>();
            //temp.Add(new Test() { AlarmType = "hello", AlarmTime = "hi", ID = 1 });
            //temp.Add(new Test() { AlarmType = "thing", AlarmTime = "yup", ID = 2 });
            //AlarmGrid.ItemsSource = temp;
        }



        //private delegate void UpdateTextCallback(string message); 

        //public void SetTextBox(string s)
        //{
        //    textBlock.Dispatcher.Invoke(
        //        new UpdateTextCallback(SetText),
        //        new object[] { s });
        //}

        //private void SetText(string s)
        //{
        //    textBlock.Text = s;
        //}

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

        private void RedXHit(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void EditAlarm(object sender, RoutedEventArgs e)
        {
            var temp = (int)((Button)sender).CommandParameter;
            var alarm = Alarm.GetAlarm(temp);
            if (alarm.AlarmType == "Daily") 
                new Daily(Alarm, this, alarm.GetAlarm(), alarm.ID);
            if (alarm.AlarmType == "Weekly")
                new Weekly(Alarm, this, ((WeeklyAlarm)alarm).GetDays() ,alarm.GetAlarm(), alarm.ID);

            Console.WriteLine("");
        }

        private void DeleteAlarm(object sender, RoutedEventArgs e)
        {
            var temp = (int)((Button)sender).CommandParameter;
            Alarm.RemoveAlarm(temp);
            Console.WriteLine("");
        }

        private void AddDailyAlarm(object sender, RoutedEventArgs e)
        {
            new Daily(Alarm, this);
        }

        private void AddWeeklyAlarm(object sender, RoutedEventArgs e)
        {
            new Weekly(Alarm, this);
        }

        private void AddRandomAlarm(object sender, RoutedEventArgs e)
        {

        }
    }
}
