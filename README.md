# FSF (Fixed Size File)

FSF is a netstandard 2.0 library to use in order to parse and build Fixed Size Files to your custom POCO Classes or the provided POCO class FileRecords.

The Library exposes the following :

```C#
class DataFieldAttribute // Used To Decorate properties of POCO Classes
```
```C#
class Field // Represent the Fields of a Row from a File
```
```C#
class FileModel // Represents the Fields of The File (Header, Records, Trailer)
```
```C#
class FileRecords // Represents the Parsed File Rows Values
```
```C#
class FParser // Contains Extension methods to Parse Files to FileRecords Objects
```
```C#
class FBuilder // Contains extension methods that will Create or Write a File from the FileRecords object provided
```

# Parsing Files Using a JSONString.

given a json file with the format,

```JSON
{
  "Header": 
    [
      {
        "Name": "HeaderIdentifier",
        "Length": 1,
        "Position": 1,
        "DefaultValue": "1"
      },
      {
        "Name": "FileDate",
        "Length": 8,
        "Position": 2,
        "DefaultValue": null
      },
      {
        "Name": "Filler",
        "Length": 10,
        "Position": 35,
        "DefaultValue": " "
      }
    ]
  ,
  "Details": 
    [
      {
        "Name": "DetailIdentifier",
        "Length": 1,
        "Position": 1,
        "DefaultValue": "2"
      },
      {
        "Name": "AccountNumber",
        "Length": 19,
        "Position": 2,
        "DefaultValue": null
      },
      {
        "Name": "ExpirationDate",
        "Length": 4,
        "Position": 21,
        "DefaultValue": null
      }
    ]
  ,
  "Trailer": [
    {
      "Name": "TrailerIdentifier",
      "Length": 1,
      "Position": 1,
      "DefaultValue": "T"
    },
    {
      "Name": "RecordCount",
      "Length": 9,
      "Position": 2,
      "DefaultValue": null
    }
]
}

```

```C#

//Create instance of File Model using the JSONString
var fileModel = new FileModel(jsonString);
//Using Extension method of File Model to FileRecords of File (Either FilePath or IEnumerable<string> FileLines)
FileRecords fileRecords = fileModel.GetFileRecords(FullPathOfFile);

```

# Parsing Files Using a Custom POCO Class with DataField Attribute Decorated Properties.

If we have a POCO Class such as,

```C#

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

```

Using Generic Static Method Of FParser

```C#

//Retrieved Records in Enumerable Custom POCO Class
IEnumerable<TestFileModel> fileRecords = FParser.GetFileRecords<TestFileModel>(FullPathOfFile);

```
