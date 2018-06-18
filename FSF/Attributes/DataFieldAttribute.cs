using FSF.Models;
using System;

namespace FSF.Attributes
{
    /// <summary>
    /// Used to decorate Propeties of Class in order to dynamically parse a File
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DataFieldAttribute : Attribute
    {
        private readonly FieldPosition _placePosition;
        private readonly int _position;
        private readonly int _length;

        public DataFieldAttribute(FieldPosition PlacePosition, int Position, int Length)
        {
            _placePosition = PlacePosition;
            _position      = Position;
            _length        = Length;
        }

        /// <summary>
        /// Sets the position of the Field (Header, Detail or Trailer)
        /// </summary>
        public FieldPosition PlacePosition { get { return _placePosition; } }

        /// <summary>
        ///  Sets the line position of the Field
        /// </summary>
        public int Position { get { return _position; } }

        /// <summary>
        /// Set the Lentgh of the Parsed Field
        /// </summary>
        public int Length { get { return _length; } }
    }
}
