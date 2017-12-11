using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PFLApp.Models
{
    /// <summary>
    /// Represents a single PFL Product Definition
    /// </summary>
    public class PflProductModel
    {
        /// <summary>
        /// Internal Product Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// External Product Id
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// Name of Product
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Description of Product
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Image URL representing Product
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// Default quantity size
        /// </summary>
        public int QuantityDefault { get; set; }
        /// <summary>
        /// List of Delivery Pricings
        /// </summary>
        public List<PflDeliveryPriceModel> DeliveredPrices { get; set; }
    }
}
