using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.IO;

namespace UTE.Testing.Unit.Parsing.Helper
{
	public static class ParseHelper
	{
        public static T DeserializeJSON<T>(string targetFilePath)
        {
            JsonSerializerSettings Settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            using (StreamReader sr = new StreamReader(targetFilePath))
            {
                return JsonConvert.DeserializeObject<T>(sr.ReadToEnd(), Settings);
            }
        }
    }

    public class DataSheetMeter
    {
        [JsonProperty("pos")]
        public DataSheet3Dim Pos { get; set; }
        [JsonProperty("txreFilename")]
        public string TextureFileName { get; set; }
    }

    public class DataSheetMeterCombined
    {
        [JsonProperty("meterRootObject")]
        public DataSheetMeter MeterRootObj { get; set; }
        [JsonProperty("meterBar")]
        public DataSheetMeter MeterBar { get; set; }
    }

    public class DataSheet3Dim
    {
        [JsonProperty("dim3")]
        public float[] Dim3 { get; set; } = new float[3];
    }
}