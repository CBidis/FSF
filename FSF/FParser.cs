using FSF.Helpers;
using FSF.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

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

        /// <summary>
        /// Get Ienumerable lines as T object
        /// </summary>
        /// <typeparam name="T">The object from which the fields will be parsed</typeparam>
        /// <param name="filePath">path to file</param>
        /// <returns>IEnumerable<T> of records</returns>
        public static IEnumerable<T> GetFileRecords<T>(string filePath) where T : class
        {
            FileModel fileModel = Utils.GetFileModelFromType(typeof(T));
            string[] fileLines = File.ReadAllLines(filePath);
            return FileRecords<T>(fileModel, fileLines);
        }

        /// <summary>
        /// Get Ienumerable lines as T object
        /// </summary>
        /// <typeparam name="T">The object from which the fields will be parsed</typeparam>
        /// <param name="fileLines">file lines</param>
        /// <returns>IEnumerable<T> of records</returns>
        public static IEnumerable<T> GetFileRecords<T>(IEnumerable<string> fileLines) where T : class
        {
            FileModel fileModel = Utils.GetFileModelFromType(typeof(T));
            return FileRecords<T>(fileModel, fileLines.ToArray());
        }

        private static IEnumerable<T> FileRecords<T>(FileModel fileModel, string[] fileLines) where T : class
        {
            var startIndex = 0;
            var endIndex = fileLines.Length;

            var enumerableOfT = new List<T>();

            if (fileModel.HeaderFields != null)
            {
                enumerableOfT.Add(GetAssignedTFromLine<T>(fileLines[startIndex], fileModel.HeaderFields));
                startIndex++;
            }

            if (fileModel.TrailerFields != null)
                endIndex--;

            for (var i = startIndex; i < endIndex; i++)
            {
                enumerableOfT.Add(GetAssignedTFromLine<T>(fileLines[i], fileModel.RecordFields));
            }

            if (fileModel.TrailerFields != null)
                enumerableOfT.Add(GetAssignedTFromLine<T>(fileLines[endIndex], fileModel.TrailerFields));

            return enumerableOfT;
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

        private static T GetAssignedTFromLine<T>(string line, List<Field> modelFields)
        {
            var instantiatedT = (T)Activator.CreateInstance(typeof(T));

            foreach (Field modelField in modelFields)
            {
                var valueToAssing = line.Substring(modelField.Position - 1, modelField.Length);
                PropertyInfo propertyInfo = instantiatedT.GetType().GetProperty(modelField.Name);

                if (propertyInfo != null)
                    propertyInfo.SetValue(instantiatedT, line.Substring(modelField.Position - 1, modelField.Length), null);
            }

            return instantiatedT;
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
