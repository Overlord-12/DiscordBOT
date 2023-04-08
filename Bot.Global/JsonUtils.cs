using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Global
{
	public class JsonUtils
	{
        public static T ReadJson<T>(string filePath)
        {
            string json = string.Empty;
            using (var fs = File.OpenRead(filePath))
            {
                using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                {
                    json = sr.ReadToEnd();
                }
            }

            T result = JsonConvert.DeserializeObject<T>(json);
            return result;
        }
    }
}
