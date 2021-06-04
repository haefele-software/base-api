using System;

namespace ProjectName.Common.Auditing
{
    /// <summary>
    /// Specifies the class this attribute is applied to requires authorization.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class AuditableAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuditableAttribute"/> class. 
        /// </summary>
        public AuditableAttribute() { }
    }
}
