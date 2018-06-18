using FSF.Attributes;

namespace FSF.UnitTests.Models
{
    public class TestFileModel
    {
        [DataField(PlacePosition: FSF.Models.FieldPosition.Header, Length : 1, Position : 1)]
        public string HeaderIdentifier { get; set; }

        [DataField(PlacePosition : FSF.Models.FieldPosition.Header, Length : 8, Position : 2)]
        public string FileDate { get; set; }

        [DataField(PlacePosition : FSF.Models.FieldPosition.Header, Length : 35, Position : 10)]
        public string Filler { get; set; }

        [DataField(PlacePosition : FSF.Models.FieldPosition.Detail, Length : 1, Position : 1)]
        public string DetailIdentifier { get; set; }

        [DataField(PlacePosition : FSF.Models.FieldPosition.Detail, Length : 19, Position : 2)]
        public string AccountNumber { get; set; }

        [DataField(PlacePosition : FSF.Models.FieldPosition.Detail, Length : 4, Position : 21)]
        public string ExpirationDate { get; set; }

        [DataField(PlacePosition : FSF.Models.FieldPosition.Trailer, Length : 1, Position : 1)]
        public string TrailerIdentifier { get; set; }

        [DataField(PlacePosition : FSF.Models.FieldPosition.Trailer, Length : 9, Position : 2)]
        public string RecordCount { get; set; }
    }
}
