using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace YellAtMe
{
    public class AlarmTimer
    {
        private List<TimeForAlarm> TimeForAlarms;
        private MainWindow Window;
        private static MediaPlayer SoundPlayer;
        //private DailyAlarm Temp;

        public AlarmTimer(MainWindow window)
        {
            TimeForAlarms = new List<TimeForAlarm>();
            window.AlarmGrid.ItemsSource = TimeForAlarms;

            //for testing display
            var temp = new DailyAlarm(5,34);
            TimeForAlarms.Add(temp);
            var days = new List<DayOfWeek>();
            days.Add(DayOfWeek.Monday);
            days.Add(DayOfWeek.Friday);
            days.Add(DayOfWeek.Wednesday);
            var temp2 = new WeeklyAlarm(days, 7,3);
            TimeForAlarms.Add(temp2);
            var temp3 = new RandomAlarm(2019, 5, 3, 18, 44);
            TimeForAlarms.Add(temp3);
            SetIDs();

            var timer = new Timer();
            timer.Elapsed += timerTriggered;
            timer.Interval = 1000;
            timer.Start();
            Window = window;
        }

        #region alarmStuff
        public void AddAlarm(TimeForAlarm alarm)
        {
            TimeForAlarms.Add(alarm);
            SetIDs();
        }

        public List<TimeForAlarm> GetAlarms()
        {
            return TimeForAlarms;
        }

        public TimeForAlarm GetAlarm(int id)
        {
            return TimeForAlarms[id];
        }

        public void RemoveAlarm(int id)
        {
            TimeForAlarms.RemoveAt(id);
            SetIDs();
            Window.AlarmGrid.Items.Refresh();
        }

        private void SetIDs()
        {
            for (int i = 0; i < TimeForAlarms.Count; i++)
            {
                TimeForAlarms[i].ID = i;
            }
        }
        #endregion
        
        private void timerTriggered(object sender, ElapsedEventArgs e)
        {
            //Window.SetTextBox(DateTime.Now.ToString());

            //if(!Temp.getTriggered())
            //    Temp.AlarmWentOff();



            foreach (var item in TimeForAlarms)
            {
                if (item.AlarmTriggered() && !item.getTriggered())
                {
                    ShowAlarm(item.AlarmWentOff());
                }
            }
        }

        public void ShowAlarm(TimeForAlarm alarm)
        {
            //using a balloon is a mess, going to just make a new window instead
            //string text = alarm.AlarmText;
            //if (text == "")
            //    text = "Alarm Went Off";
            //Window.NotifyIcon.ShowBalloonTip("Alarm Went Off", text, 
            //    Hardcodet.Wpf.TaskbarNotification.BalloonIcon.None);
            //if (alarm.AlarmHasSound)
            //{
            //    Window.NotifyIcon.TrayBalloonTipShown += (sender, e) => OpenBalloon(sender, e, alarm.GetAlarmSound());
            //    //Window.NotifyIcon.TrayBalloonTipClosed += (sender, e) => CloseBalloon(sender, e);
            //}
        }

        private void OpenBalloon(object sender, EventArgs e, string sound)
        {
            SoundPlayer = new MediaPlayer();
            SoundPlayer.Open(new Uri(sound));
            SoundPlayer.Play();
            SoundPlayer.MediaEnded += LoopAlarm;
        }

        private void CloseBalloon(object sender, EventArgs e)
        {
            Console.WriteLine("");
            SoundPlayer.Stop();
            
        }

        private void LoopAlarm(object sender, EventArgs e)
        {
            MediaPlayer player = (MediaPlayer)sender;
            player.Position = TimeSpan.Zero;
            player.Volume -= 5;
            player.Play();

        }
    }
}
