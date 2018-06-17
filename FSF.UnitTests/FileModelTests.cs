using FSF.Models;
using System;
using Xunit;

namespace FSF.UnitTests
{
    public class FileModelTests
    {
        [Fact]
        public  void TestModelParse()
        {
            var fileAsString = TestHelpers.SpecsTXTFile;
            var fileModel = new FileModel(fileAsString);

            Assert.NotNull(fileModel.HeaderFields);
            Assert.NotNull(fileModel.RecordFields);
            Assert.NotNull(fileModel.TrailerFields);
        }

        [Fact]
        public void TestFailModelParse() => Assert.Throws<ArgumentException>(() => new FileModel("{}"));
    }
}
