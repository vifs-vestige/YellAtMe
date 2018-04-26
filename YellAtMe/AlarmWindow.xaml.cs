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
    /// Interaction logic for AlarmWindow.xaml
    /// </summary>
    public partial class AlarmWindow : Window
    {
        private TimeForAlarm Alarm;
        private MediaPlayer SoundPlayer;
        private bool AlarmStopped = false;

        public AlarmWindow(TimeForAlarm alarm)
        {
            InitializeComponent();
            Show();
            Alarm = alarm;
            AlarmText.Text = alarm.AlarmText;
            if (alarm.AlarmHasSound)
                AlarmSound();
        }

        public AlarmWindow()
        {
            InitializeComponent();
            Show();
        }
        

        private void AlarmSound()
        {
            SoundPlayer = new MediaPlayer();
            SoundPlayer.Open(new Uri(Alarm.GetAlarmSound()));
            SoundPlayer.Play();
            SoundPlayer.MediaEnded += LoopAlarm;
        }

        private void LoopAlarm(object sender, EventArgs e)
        {
            MediaPlayer player = (MediaPlayer)sender;
            player.Position = TimeSpan.Zero;
            if (!player.Volume.Equals(0.0))
            {
                player.Volume -= .05;
                player.Play();
            }
            else
            {
                AlarmStopped = true;
                ((MediaPlayer)sender).MediaEnded -= LoopAlarm;
            }
        }

        private void CloseRight(object sender, CancelEventArgs e)
        {
            if (Alarm.AlarmHasSound && AlarmStopped == false)
                SoundPlayer.Stop();
        }


    }
}
