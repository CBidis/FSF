using FSF.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace FSF.Helpers
{
    internal static class FileUtils
    {
        internal static string FileModelJSONString(this FileModel fileModel) => JsonConvert.SerializeObject(fileModel);

        internal static (List<Field>, List<Field>, List<Field>) ParseJsonModel(string jsonString)
        {
            List<Field> header  = null;
            List<Field> trailer = null;
            List<Field> records = null;

            var jsonObject = JObject.Parse(jsonString);

            foreach (JProperty item in jsonObject.Children())
            {
                if (item.Name == "Header")
                    header = JsonConvert.DeserializeObject<List<Field>>(item.Value.ToString());
                else if (item.Name == "Details")
                    records = JsonConvert.DeserializeObject<List<Field>>(item.Value.ToString());
                else if (item.Name == "Trailer")
                    trailer = JsonConvert.DeserializeObject<List<Field>>(item.Value.ToString());
                else
                    throw new ArgumentException($"Deserialized object does not contain any of the valid Childrens (Header, Details, Trailer) --> {item.Name} not supported");
            }

            if((records == null))
                throw new ArgumentException($"Json Object {jsonObject} does not contain Records fields");

            return (header, trailer, records);
        }
    }
}
