using FSF.Models;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FSF.UnitTests
{
    public class FBuilderTests
    {
        [Fact]
        public void BuildFileLines()
        {
            var fileRecords = new FileRecords()
            {
                Header = TestHelpers.GetHeaderTestFields,
                Trailer = TestHelpers.GetTrailerTestFields,
                Records = TestHelpers.GetRecordTestFields
            };

            IEnumerable<string> fileLines = fileRecords.GetFileLines();
            Assert.True(fileLines.ToList().Count == 4);
        }
    }
}
