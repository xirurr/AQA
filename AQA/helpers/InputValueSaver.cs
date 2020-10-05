using System.IO;
using Newtonsoft.Json;

namespace AQA.helpers
{
    public class InputValueSaver
    {
        public static string SaveToTmpFile(InputValue inputValue)
        {
            var tmpFileName = (Path.GetTempFileName());
            var serialize_object = JsonConvert.SerializeObject(inputValue, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            using(var sw = new StreamWriter(tmpFileName))
            {
                sw.Write(serialize_object);
            }

            return tmpFileName;
        }
    }
}