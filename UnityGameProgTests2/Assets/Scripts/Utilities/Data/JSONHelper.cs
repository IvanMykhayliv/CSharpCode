using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.IO;

public static class JSONHelper
{
	public static T DeserializeJSON<T>(string targetFilePath)
	{
        JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            Converters = new List<JsonConverter>
            {
                    new StringEnumConverter()
                    {
                        NamingStrategy = new CamelCaseNamingStrategy()
                    }
            }
        };

        using (StreamReader sr = new StreamReader(targetFilePath))
        {
            return JsonConvert.DeserializeObject<T>(sr.ReadToEnd(), Settings);
        }
	}
}