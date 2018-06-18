using FSF.Models;
using FSF.UnitTests.Models;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FSF.UnitTests
{
    public class FParserTests
    {
        [Fact]
        public static void ParseFileRecords()
        {
            var fileModel = new FileModel(TestHelpers.SpecsTXTFile);
            FileRecords fileRecords = fileModel.GetFileRecords(TestHelpers.AllLinesOfTestFile);
            Assert.True((fileRecords.Records.Count == 2 && fileRecords.Header != null && fileRecords.Trailer != null));
        }

        [Fact]
        public static void ParseFileRecordsFromFile()
        {
            var fileModel = new FileModel(TestHelpers.SpecsTXTFile);
            FileRecords fileRecords = fileModel.GetFileRecords(TestHelpers.PathOfTestFile);
            Assert.True((fileRecords.Records.Count == 2 && fileRecords.Header != null && fileRecords.Trailer != null));
        }

        [Fact]
        public static void ParseFileRecordsFromType()
        {
            IEnumerable<TestFileModel> fileRecords = FParser.GetFileRecords<TestFileModel>(TestHelpers.PathOfTestFile);
            Assert.True((fileRecords.ToList().Count == 4));
        }

    }
}
