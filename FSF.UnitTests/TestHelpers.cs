using FSF.Models;
using System.Collections.Generic;
using System.IO;

namespace FSF.UnitTests
{
    public static class TestHelpers
    {
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
