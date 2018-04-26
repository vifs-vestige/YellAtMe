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

        public AlarmTimer(MainWindow window)
        {
            TimeForAlarms = new List<TimeForAlarm>();
            window.AlarmGrid.ItemsSource = TimeForAlarms;

            //for testing display
            //var temp = new DailyAlarm(5,34);
            //TimeForAlarms.Add(temp);
            //var days = new List<DayOfWeek>();
            //days.Add(DayOfWeek.Monday);
            //days.Add(DayOfWeek.Friday);
            //days.Add(DayOfWeek.Wednesday);
            //var temp2 = new WeeklyAlarm(days, 7,3);
            //TimeForAlarms.Add(temp2);
            //var temp3 = new RandomAlarm(2019, 5, 3, 18, 44);
            //TimeForAlarms.Add(temp3);

            var timer = new Timer();
            timer.Elapsed += timerTriggered;
            timer.Interval = 1000;
            timer.Start();
            Window = window;
            
            

            SetIDs();
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
        }

        private void SetIDs()
        {
            for (int i = 0; i < TimeForAlarms.Count; i++)
            {
                TimeForAlarms[i].ID = i;
            }
            Window.AlarmGrid.Items.Refresh();
        }
        #endregion
        
        private void timerTriggered(object sender, ElapsedEventArgs e)
        {
            foreach (var item in TimeForAlarms)
            {
                if (item.AlarmTriggered() && !item.getTriggered())
                {
                    item.AlarmWentOff();
                }
            }
        }

    }
}
