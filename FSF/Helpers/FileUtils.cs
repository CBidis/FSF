using FSF.Models;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace FSF.Helpers
{
    internal static class FileUtils
    {
        internal static string FileModelJSONString(this FileModel fileModel) => JsonSerializer.Serialize(fileModel);

        internal static (List<Field>, List<Field>, List<Field>) ParseJsonModel(string jsonString)
        {
            List<Field> header  = null;
            List<Field> trailer = null;
            List<Field> records = null;

            if ((string.IsNullOrEmpty(jsonString)))
                throw new ArgumentNullException("json string is empty or null");

            var jsonDocument = JsonSerializer.Deserialize<JsonElement>(jsonString);

            var hasHeader = jsonDocument.TryGetProperty("Header", out var headerProp);
            var hasDetails = jsonDocument.TryGetProperty("Details", out var detailsProp);
            var hasTrailer = jsonDocument.TryGetProperty("Trailer", out var trailerProp);

            if (!hasDetails)
            {
                throw new ArgumentException("Has no records");
            }

            var headerRaw = hasHeader ? headerProp.GetRawText() : string.Empty;
            var detailsRaw = detailsProp.GetRawText();
            var trailerRaw = hasTrailer ? trailerProp.GetRawText() : string.Empty;

            if (!string.IsNullOrEmpty(headerRaw))
                header = JsonSerializer.Deserialize<List<Field>>(headerRaw);

            if (!string.IsNullOrEmpty(detailsRaw))
                records = JsonSerializer.Deserialize<List<Field>>(detailsRaw);

            if (!string.IsNullOrEmpty(trailerRaw))
                trailer = JsonSerializer.Deserialize<List<Field>>(trailerRaw);


            if((records == null))
                throw new ArgumentException($"Json Object {jsonDocument} does not contain Records fields");

            return (header, trailer, records);
        }
    }
}
