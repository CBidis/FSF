namespace FSF.Models
{
    /// <summary>
    /// Represents the field of a row
    /// </summary>
    public class Field
    {
        /// <summary>
        /// the name of the Field
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Length of the Field
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// Position of the Field in row
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        /// DefaultValue of the Field
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// Value of the Field
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Ovveriden to string method
        /// </summary>
        /// <returns>Name of Fiedld</returns>
        public override string ToString() => Name;

        /// <summary>
        /// returns a copy of Current Field object
        /// </summary>
        /// <returns>a copy of Current Field object</returns>
        internal Field Clone() => new Field
        {
            DefaultValue = this.DefaultValue,
            Name = this.Name,
            Length = this.Length,
            Position = this.Position,
            Value = this.Value
        };
    }
}
