using FSF.Models;
using FSF.UnitTests.Models;
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

        [Fact]
        public void BuildFileLinesWithGlobalFunction()
        {
            var fileRecords = new FileRecords()
            {
                Header = TestHelpers.GetHeaderTestFields,
                Trailer = TestHelpers.GetTrailerTestFields,
                Records = TestHelpers.GetRecordTestFields
            };

            IEnumerable<string> fileLines = fileRecords.GetFileLines(field => 
            {
                if (field.Name == "Name")
                    return "NONAME    ";
                else
                    return field.Value;
            });

            Assert.True(fileLines.ToList().Count == 4);
            Assert.StartsWith("NONAME", fileLines.ToList()[1]);
            Assert.StartsWith("NONAME", fileLines.ToList()[2]);
        }

        [Fact]
        public void BuildFileLinesGeneric()
        {
            IEnumerable<string> fileLines = TestHelpers.EnumerableObjects.GetFileLines<TestFileModel>();
            Assert.True(fileLines.ToList().Count == 5);
        }

        [Fact]
        public void BuildFileLinesGenericWithGlobalFunction()
        {
            IEnumerable<string> fileLines = TestHelpers.EnumerableObjects.GetFileLines<TestFileModel>(((objectRow) => 
            {
                if (objectRow.DetailIdentifier == "2")
                    objectRow.DetailIdentifier = "9";
                return objectRow;
            }
            ));

            Assert.True(fileLines.ToList().Count == 5);
            Assert.StartsWith("9", fileLines.ToList()[1]);
            Assert.StartsWith("9", fileLines.ToList()[2]);
            Assert.StartsWith("9", fileLines.ToList()[3]);
        }
    }
}
