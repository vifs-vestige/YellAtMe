using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace YellAtMe
{
    class SaveLoad
    {
        private static string FilePath = "Alarms.txt";

        public static void Save(List<TimeForAlarm> alarms)
        {
            FileStream outFile = File.Create(FilePath);
            XmlSerializer formatter = new XmlSerializer(alarms.GetType());
            formatter.Serialize(outFile, alarms);
        }

        public static List<TimeForAlarm> Load()
        {
            var alarms = new List<TimeForAlarm>();
            XmlSerializer formatter = new XmlSerializer(alarms.GetType());
            using (FileStream fileStream = new FileStream(FilePath, FileMode.OpenOrCreate))
            {
                if (fileStream.Length != 0)
                {
                    byte[] buffer = new byte[fileStream.Length];
                    fileStream.Read(buffer, 0, (int)fileStream.Length);
                    MemoryStream stream = new MemoryStream(buffer);
                    alarms = (List<TimeForAlarm>)formatter.Deserialize(stream);
                }
            }
            return alarms;
        }
        
    }
}
