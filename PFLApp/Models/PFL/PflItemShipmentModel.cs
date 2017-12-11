namespace PFLApp.Models
{
    /// <summary>
    /// Represents the mapping between a Shipment and an Item
    /// </summary>
    public class PflItemShipmentModel
    {
        /// <summary>
        /// Reference to sequence number of item in order
        /// </summary>
        public int ItemSequenceNumber { get; set; }
        /// <summary>
        /// Reference to sequence number of shipment in order
        /// </summary>
        public int ShipmentSequenceNumber { get; set; }
    }
}