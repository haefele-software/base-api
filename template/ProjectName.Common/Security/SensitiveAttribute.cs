using System;

namespace ProjectName.Common.Security
{
    /// <summary>
    /// Specifies the class this attribute is applied to requires authorization.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class SensitiveAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SensitiveAttribute"/> class. 
        /// </summary>
        public SensitiveAttribute() { }

        public bool Encrypt { get; set; } = true;
    }
}
