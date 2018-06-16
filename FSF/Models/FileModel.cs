using FSF.Helpers;
using System.Collections.Generic;

namespace FSF.Models
{
    /// <summary>
    /// Represents the specified Fields of a File.
    /// </summary>
    public class FileModel
    {
        private readonly List<Field> _headerFields;
        private readonly List<Field> _trailerFields;
        private readonly List<Field> _recordsFields;

        /// <summary>
        ///  Given a JSON string from which the FileModel Properties will be builded
        /// </summary>
        /// <param name="jsonString">a parsed json string</param>
        public FileModel(string jsonString)
        {
            (List<Field> headerFields, List<Field> trailerFields, List<Field> recordFields) = FileUtils.ParseJsonModel(jsonString);

            _headerFields  = headerFields;
            _trailerFields = trailerFields;
            _recordsFields = recordFields;
        }

        /// <summary>
        /// Creates instance of fileModel from given collections of Fields
        /// </summary>
        /// <param name="headerFields">Collection of Header fields</param>
        /// <param name="recordFields">Collection of record fields</param>
        /// <param name="trailerFields">Collection of trailer fields</param>
        public FileModel(List<Field> headerFields, List<Field> recordFields, List<Field> trailerFields)
        {
            _headerFields = headerFields;
            _recordsFields = recordFields;
            _trailerFields = trailerFields;
        }

        /// <summary>
        /// A collection of the Header Fields
        /// </summary>
        public List<Field> HeaderFields { get { return _headerFields; }}

        /// <summary>
        /// A collection of the Record Fields
        /// </summary>
        public List<Field> RecordFields { get { return _recordsFields; }}

        /// <summary>
        /// A collection of the Trailer Fields
        /// </summary>
        public List<Field> TrailerFields { get { return _trailerFields; }}

        /// <summary>
        /// Retuns json string of File Model
        /// </summary>
        /// <returns>Retuns json string of File Model</returns>
        public override string ToString() => this.FileModelJSONString();
    }
}
