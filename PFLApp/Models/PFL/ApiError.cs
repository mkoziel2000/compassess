using System.Collections.Generic;

namespace PFLApp.Models.PFL
{
    /// <summary>
    /// Represents a Response Error
    /// </summary>
    public class ApiError
    {
        /// <summary>
        /// Element containing the error
        /// </summary>
        public string DataElement { get; set; }
        /// <summary>
        /// List of errors associated with element
        /// </summary>
        public List<string> DataElementErrors { get; set; }
    }
}