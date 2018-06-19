using FSF.Helpers;
using FSF.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
        /// Generic Method to write from Ienumerable<T> objects
        /// </summary>
        /// <typeparam name="T">T object</typeparam>
        /// <param name="fileRecords">Ienumerable<T> objects</param>
        /// <param name="pathToWrite">Full Path of file to write</param>
        /// <param name="globalFieldBuilder">The Function that will be applied to all field values</param>
        public static void WriteFile<T>(this IEnumerable<T> fileRecords, string pathToWrite, Func<T, T> globalFieldBuilder = null) where T : class
        {
            FileModel fileModel = Utils.GetFileModelFromType(typeof(T));
            IEnumerable<string> fileLines = FileLines<T>(fileRecords.ToList(), fileModel, globalFieldBuilder);
            File.WriteAllLines(pathToWrite, fileLines);
        }

        /// <summary>
        /// Get IEnumerable File Lines (IEnumerable<string>)
        /// </summary>
        /// <param name="fileRecords">File Records of File to build</param>
        /// <param name="globalFieldBuilder">The Function that will be applied to all field values</param>
        /// <returns>Ienumerable<string> of file lines</returns>
        public static IEnumerable<string> GetFileLines(this FileRecords fileRecords, Func<Field, string> globalFieldBuilder = null) => FileLines(fileRecords, globalFieldBuilder);

        /// <summary>
        /// Generic method to Get IEnumerable File Lines (IEnumerable<string>)
        /// </summary>
        /// <typeparam name="T">T object</typeparam>
        /// <param name="fileRecords">Ienumerable<T> objects</param>
        /// <param name="globalFieldBuilder">The Function that will be applied to all field values</param>
        /// <returns></returns>
        public static IEnumerable<string> GetFileLines<T>(this IEnumerable<T> fileRecords, Func<T, T> globalFieldBuilder = null)
        {
            FileModel fileModel = Utils.GetFileModelFromType(typeof(T));
            return FileLines<T>(fileRecords.ToList(), fileModel, globalFieldBuilder);
        }

        private static IEnumerable<string> FileLines(FileRecords fileRecords, Func<Field, string> globalFieldBuilder = null)
        {
            var fileLines = new List<string>();

            if (fileRecords.Header != null)
                fileLines.Add(BuildLineFromFields(fileRecords.Header, globalFieldBuilder));

            foreach (List<Field> record in fileRecords.Records.Values)
            {
                fileLines.Add(BuildLineFromFields(record, globalFieldBuilder));
            }

            if (fileRecords.Trailer != null)
                fileLines.Add(BuildLineFromFields(fileRecords.Trailer, globalFieldBuilder));

            return fileLines;
        }

        private static IEnumerable<string> FileLines<T>(List<T> fileRecords, FileModel fileModel, Func<T, T> globalFieldBuilder = null)
        {
            var startIndex = 0;
            var endIndex = fileRecords.Count;
            var fileLines = new List<string>();

            if (fileModel.HeaderFields != null)
            {
                fileLines.Add(BuildLineFromT<T>(fileRecords[startIndex], fileModel.HeaderFields, globalFieldBuilder));
                startIndex++;
            }

            if (fileModel.TrailerFields != null)
                endIndex--;

            for (var i = startIndex; i < endIndex; i++)
            {
                fileLines.Add(BuildLineFromT<T>(fileRecords[i], fileModel.RecordFields, globalFieldBuilder));
            }

            if (fileModel.TrailerFields != null)
                fileLines.Add(BuildLineFromT<T>(fileRecords[endIndex], fileModel.TrailerFields, globalFieldBuilder));

            return fileLines;
        }

        private static string BuildLineFromFields(List<Field> fields, Func<Field, string> globalFieldBuilder = null)
        {
            var lineBuilder = new StringBuilder();
            fields.ForEach(field => lineBuilder.Append(globalFieldBuilder == null ? field.Value : globalFieldBuilder(field)));
            return lineBuilder.ToString();
        }

        private static string BuildLineFromT<T>(T objectData, List<Field> modelFields, Func<T, T> globalFieldBuilder = null)
        {
            var lineBuilder = new StringBuilder();

            foreach(Field field in modelFields)
            {
                object valueToAppend = string.Empty;

                PropertyInfo propertyInfo = objectData.GetType().GetProperty(field.Name);

                if (propertyInfo != null)
                {
                    objectData = globalFieldBuilder == null ? objectData : globalFieldBuilder(objectData);
                    valueToAppend = propertyInfo.GetValue(objectData, null);
                    lineBuilder.Append(valueToAppend);
                }

            }

            return lineBuilder.ToString();
        }
    }
}
