using System.Xml.Serialization;
using System.IO;

namespace ASNA
{
    public class Settings
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string HostIP { get; set; }
        public string Port { get; set; }
        public string GenericName { get; set; }
        public bool SkipICMPScan { get; set; }

        public static void SaveData(object obj, string filename)
        {
            // pass in OBJ (entire class), save the data to an XML file
            XmlSerializer sr = new XmlSerializer(obj.GetType());
            TextWriter writer = new StreamWriter(filename);
            sr.Serialize(writer, obj);
            writer.Close();
        }
    }
}
