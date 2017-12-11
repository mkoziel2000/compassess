namespace PFLApp.Models
{
    /// <summary>
    /// Represents Pricing of A product for a specific delivery method
    /// </summary>
    public class PflDeliveryPriceModel
    {
        /// <summary>
        /// Internal Delivery Method Code
        /// </summary>
        public string DeliveryMethodCode { get; set; }
        /// <summary>
        /// Descrption of Delivery Method
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Price to deliver using delivery method
        /// </summary>
        public float Price { get; set; }
        /// <summary>
        /// Is Delivery method the default?
        /// </summary>
        public bool IsDefault { get; set; }
    }
}