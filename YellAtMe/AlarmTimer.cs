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
        private MainWindow Window;

        public AlarmTimer(MainWindow window)
        {
            var timer = new Timer();
            timer.Elapsed += timerTriggered;
            timer.Interval = 1000;
            timer.Start();
            Window = window;
            
        }

        public delegate void UpdateTextCallback(string message);

        private void timerTriggered(object sender, ElapsedEventArgs e)
        {
            Window.SetTextBox(DateTime.Now.ToString());
        }
    }
}
