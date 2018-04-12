﻿using System;
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

        public Daily(AlarmTimer alarm, MainWindow window)
        {
            InitializeComponent();
            Window = window;
            Alarm = alarm;
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            var time = (DateTime)Time.Value;
            var temp = new DailyAlarm(time.Hour, time.Minute);
            Alarm.AddAlarm(temp);
            Window.Show();
            Close();
        }


        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Closing(object sender, CancelEventArgs e)
        {
            Window.Show();
        }

    }
}