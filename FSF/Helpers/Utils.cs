using FSF.Attributes;
using FSF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FSF.Helpers
{
    public static class Utils
    {
        public static FileModel GetFileModelFromType(Type type)
        {
            IEnumerable<PropertyInfo> propertiesOfDataField = type.GetProperties().Where(
                prop => prop.GetCustomAttributes().Any(attr => attr.GetType() == typeof(DataFieldAttribute)));

            if (!propertiesOfDataField.Any())
                throw new ArgumentException($"for type {type} they have not been defined any properties with attibute of DataFieldAttribute");

            var headerFields  = new List<Field>();
            var recordFields  = new List<Field>();
            var trailerFields = new List<Field>();

            foreach(PropertyInfo property in propertiesOfDataField)
            {
                DataFieldAttribute propDataFieldAttr = property.GetCustomAttribute<DataFieldAttribute>();

                var fieldToadd = new Field { Name = property.Name, Length = propDataFieldAttr.Length, Position = propDataFieldAttr.Position };

                if (propDataFieldAttr.PlacePosition == FieldPosition.Header)
                    headerFields.Add(fieldToadd);
                else if (propDataFieldAttr.PlacePosition == FieldPosition.Detail)
                    recordFields.Add(fieldToadd);
                else
                    trailerFields.Add(fieldToadd);
            }

            return new FileModel(headerFields, recordFields, trailerFields);
        }
    }
}
