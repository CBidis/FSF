using FSF.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FSF
{
    /// <summary>
    /// A collection of Extension methods for Parsing Files to FileRecords
    /// </summary>
    public static class FParser
    {
        /// <summary>
        /// Creates file Records object
        /// </summary>
        /// <param name="fileModel">the file model from which the Line records will be parsed</param>
        /// <param name="fileLines">an enumeration of file lines</param>
        /// <returns>FileRecords of file</returns>
        public static FileRecords GetFileRecords(this FileModel fileModel, IEnumerable<string> fileLines) => FileRecords(fileModel, fileLines.ToArray());

        /// <summary>
        /// Creates file Records object
        /// </summary>
        /// <param name="fileModel">the file model from which the Line records will be parsed</param>
        /// <param name="filePath">Full Path to the file that will be parsed</param>
        /// <returns>FileRecords of file</returns>
        public static FileRecords GetFileRecords(this FileModel fileModel, string filePath)
        {
            string[] fileLines = File.ReadAllLines(filePath);
            return FileRecords(fileModel, fileLines);
        }

        private static FileRecords FileRecords(FileModel fileModel, string[] fileLines)
        {
            var startIndex = 0;
            var endIndex = fileLines.Length;
            var fileData = new FileRecords();

            if (fileModel.HeaderFields != null)
            {
                fileData.Header = GetDataFieldsFromLine(fileLines[startIndex], fileModel.HeaderFields);
                startIndex++;
            }

            if (fileModel.TrailerFields != null)
            {
                endIndex--;
                fileData.Trailer = GetDataFieldsFromLine(fileLines[endIndex], fileModel.TrailerFields);
            }
                
            for (var i = startIndex; i < endIndex; i++)
            {
                fileData.Records.Add(i, GetDataFieldsFromLine(fileLines[i], fileModel.RecordFields));
            }

            return fileData;
        }

        private static List<Field> GetDataFieldsFromLine(string line, List<Field> modelFields)
        {
            var dataFields = new List<Field>();

            foreach (Field modelField in modelFields)
            {
                Field dataField = modelField.Clone();
                dataField.Value = line.Substring(modelField.Position - 1, modelField.Length);
                dataFields.Add(dataField);
            }

            return dataFields;
        }

    }
}
