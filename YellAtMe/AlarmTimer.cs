using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace YellAtMe
{
    class AlarmTimer
    {
        private List<TimeForAlarm> TimeForAlarms;
        private MainWindow Window;
        //private DailyAlarm Temp;

        public AlarmTimer(MainWindow window)
        {
            TimeForAlarms = new List<TimeForAlarm>();
            window.AlarmGrid.ItemsSource = TimeForAlarms;
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
        }

        public List<TimeForAlarm> GetAlarms()
        {
            return TimeForAlarms;
        }

        public void RemoveAlarm(TimeForAlarm alarm)
        {
            TimeForAlarms.Remove(alarm);
        }
        #endregion
        
        private void timerTriggered(object sender, ElapsedEventArgs e)
        {
            //Window.SetTextBox(DateTime.Now.ToString());

            //if(!Temp.getTriggered())
            //    Temp.AlarmWentOff();



            //foreach (var item in TimeForAlarms)
            //{
            //    if (item.AlarmTriggered() && !item.getTriggered())
            //    {
            //        //do stuff
            //    }
            //}
        }
    }
}
