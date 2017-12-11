using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace PFLApp.Models.PFL
{
    /// <summary>
    /// Represents the results of a PFL Api Call
    /// </summary>
    public class ApiResults : ApiResponse
    {
        /// <summary>
        /// List of Errors
        /// </summary>
        public List<ApiError> Errors { get; set; }

        /// <summary>
        /// List of Messages
        /// </summary>
        public List<string> Messages { get; set; }

        /// <summary>
        /// Choosing Schema-less design so that specialization classes can reinterpret the additional elements 
        /// that are unique to each kind of response received.
        /// 
        /// This design was chosen so that there wouldn't need to be the propagation of class objects in the VS project
        /// in order for it to handle every variation on the types of objects that could be contained in the generic structure.
        /// </summary>
        [JsonExtensionData]
        public IDictionary<string,JToken> AdditionalElements { get; set; }
    }
}