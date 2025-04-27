using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;


namespace LicencesManager
{
    class AppConfig
    {
        public string Version { get; set; }
        public string DataFilePath { get; set; }



        public static AppConfig LoadConfig(string configFilePath)
        {
            if (File.Exists(configFilePath))
            {
                using (StreamReader reader = new StreamReader(configFilePath))
                {
                    return JsonConvert.DeserializeObject<AppConfig>(reader.ReadToEnd());
                }
            }
            return new AppConfig();
        }

        public void SaveConfig(string configFilePath)
        {
            using (StreamWriter writer = new StreamWriter(configFilePath))
            {
                writer.Write(JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented));
            }
        }
    }
}
