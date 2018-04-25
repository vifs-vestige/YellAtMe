using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellAtMe
{
    class AlarmWindowTools
    {

        public static string PickFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "audio files (*.mp3;*.wma;*.wav)|*.mp3;*.wma;*.wav|All files (*.*)|*.*";
            if (ofd.ShowDialog() == true)
            {
                return ofd.FileName;
            }
            return "";
        }
    }
}
