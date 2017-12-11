using Newtonsoft.Json;
using System.Collections.Generic;

namespace PFLApp.Models
{
    /// <summary>
    /// Represents a single Item/Product being ordered
    /// </summary>
    public class PflOrderItemModel
    {
        /// <summary>
        /// Sequence Number Assigned to Item
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int ItemSequenceNumber { get; set; }
        /// <summary>
        /// Product Id
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int ProductId { get; set; }
        /// <summary>
        /// Quantity of Product
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Quantity { get; set; }
        /// <summary>
        /// Production Days
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? ProductionDays { get; set; }
        /// <summary>
        /// Partner Item Reference ID
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string PartnerItemReference { get; set; }
        /// <summary>
        /// Additional data associated with Order Item
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<PflTemplateDataModel> TemplateData { get; set; }
    }
}