using FSF.Models;
using FSF.UnitTests.Models;
using System.Collections.Generic;
using System.IO;

namespace FSF.UnitTests
{
    public static class TestHelpers
    {
        public static List<TestFileModel> EnumerableObjects { get; private set; } = new List<TestFileModel>
            {
                new TestFileModel { HeaderIdentifier = "1", FileDate = "20161212", Filler = " ".PadRight(35, ' ') },
                new TestFileModel { DetailIdentifier = "2", AccountNumber = "5351178243921213789", ExpirationDate = "1212"},
                new TestFileModel { DetailIdentifier = "2", AccountNumber = "4346178243921213789", ExpirationDate = "1220"},
                new TestFileModel { DetailIdentifier = "2", AccountNumber = "6453218243921213743", ExpirationDate = "1230"},
                new TestFileModel { TrailerIdentifier = "9", RecordCount = "000000003"}
            };

        public static string SpecsTXTFile { get; private set; } = File.ReadAllText("./Files/testjsonspecs.json");

        public static string[] AllLinesOfTestFile { get; private set; } = File.ReadAllLines("./Files/testjsonfile.txt");

        public static string PathOfTestFile { get; private set; } = "./Files/testjsonfile.txt";

        public static List<Field> GetHeaderTestFields { get; private set; } = new List<Field>
        {
            new Field { Name = "Field1", Value = "H", Position = 1, Length = 1 },
            new Field { Name = "Field2", Value = "Testing ", Position = 2, Length = 8 },
        };

        public static Dictionary<int, List<Field>> GetRecordTestFields { get; private set; } = new Dictionary<int, List<Field>>
        {
            {1, new List<Field> {new Field { Name = "Name", Value = "Chris     ", Position = 1, Length = 10 }, new Field { Name = "LastName", Value = "Bidis     ", Position = 11, Length = 10 } }},
            {2, new List<Field> {new Field { Name = "Name", Value = "Fotis     ", Position = 1, Length = 10 }, new Field { Name = "LastName", Value = "Tsavalos  ", Position = 11, Length = 10 } }},
        };

        public static List<Field> GetTrailerTestFields { get; private set; } = new List<Field>
        {
            new Field { Name = "Field1", Value = "T", Position = 1, Length = 1 },
            new Field { Name = "Field2", Value = "0002", Position = 2, Length = 4 },
        };
    }
}
