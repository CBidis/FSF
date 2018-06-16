using FSF.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FSF
{
    /// <summary>
    /// Contains extension methods to Write File from FileRecords object
    /// </summary>
    public static class FBuilder
    {
        /// <summary>
        /// Writes file to specified path
        /// </summary>
        /// <param name="fileRecords">File records object for file</param>
        /// <param name="pathToWrite">Full Path of file to write</param>
        /// <param name="globalFieldBuilder">The Function that will be applied to all field values</param>
        public static void WriteFile(this FileRecords fileRecords, string pathToWrite, Func<Field, string> globalFieldBuilder = null)
        {
            IEnumerable<string> fileLines = FileLines(fileRecords, globalFieldBuilder);
            File.WriteAllLines(pathToWrite, fileLines);
        }

        /// <summary>
        /// Get IEnumerable File Lines (IEnumerable<string>)
        /// </summary>
        /// <param name="fileRecords">File Records of File to build</param>
        /// <param name="globalFieldBuilder">The Function that will be applied to all field values</param>
        /// <returns>Ienumerable<string> of file lines</returns>
        public static IEnumerable<string> GetFileLines(this FileRecords fileRecords, Func<Field, string> globalFieldBuilder = null) => FileLines(fileRecords, globalFieldBuilder);

        private static IEnumerable<string> FileLines(FileRecords fileRecords, Func<Field, string> globalFieldBuilder = null)
        {
            var fileLines = new List<string>();

            if (fileRecords.Header != null)
                BuildLineFromFields(fileRecords.Header, globalFieldBuilder);

            foreach (List<Field> record in fileRecords.Records.Values)
            {
                fileLines.Add(BuildLineFromFields(record, globalFieldBuilder));
            }

            if (fileRecords.Trailer != null)
                BuildLineFromFields(fileRecords.Trailer, globalFieldBuilder);

            return fileLines;
        }

        private static string BuildLineFromFields(List<Field> fields, Func<Field, string> globalFieldBuilder = null)
        {
            var lineBuilder = new StringBuilder();
            fields.ForEach(field => lineBuilder.Append(globalFieldBuilder == null ? field.Value : globalFieldBuilder(field)));
            return lineBuilder.ToString();
        }
    }
}
