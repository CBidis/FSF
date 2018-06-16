using FSF.Models;
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
        public static void WriteFile(this FileRecords fileRecords, string pathToWrite)
        {
            IEnumerable<string> fileLines = FileLines(fileRecords);
            File.WriteAllLines(pathToWrite, fileLines);
        }

        /// <summary>
        /// Get Ienumerable File Lines
        /// </summary>
        /// <param name="fileRecords">File Records of File to build</param>
        /// <returns>Ienumerable<string> of file lines</returns>
        public static IEnumerable<string> GetFileLines(this FileRecords fileRecords) => FileLines(fileRecords);

        private static IEnumerable<string> FileLines(FileRecords fileRecords)
        {
            var fileLines = new List<string>();

            if (fileRecords.Header != null)
                BuildLineFromFields(fileRecords.Header);

            foreach (List<Field> record in fileRecords.Records.Values)
            {
                fileLines.Add(BuildLineFromFields(record));
            }

            if (fileRecords.Trailer != null)
                BuildLineFromFields(fileRecords.Trailer);

            return fileLines;
        }

        private static string BuildLineFromFields(List<Field> fields)
        {
            var lineBuilder = new StringBuilder();
            fields.ForEach(field => lineBuilder.Append(field.Value));
            return lineBuilder.ToString();
        }
    }
}
