using System.Collections.Generic;

namespace FSF.Models
{
    /// <summary>
    /// Represents a parsed file records
    /// </summary>
    public class FileRecords
    {
        /// <summary>
        /// A collection of the Header Fields with parsed values, if Header exists
        /// </summary>
        public List<Field> Header { get; set; }

        /// <summary>
        /// Represents the Fields with parsed values per row of the File
        /// </summary>
        public Dictionary<int, List<Field>> Records { get; set; } = new Dictionary<int, List<Field>>();

        /// <summary>
        /// A collection of the Trailer Fields with parsed values, if Trailer exists
        /// </summary>
        public List<Field> Trailer { get; set; }

        /// <summary>
        /// number of rows in file without header and trailer
        /// </summary>
        /// <returns>number of rows in file without header and trailer</returns>
        public int FileRecordsCount() => Records.Count;
    }
}
