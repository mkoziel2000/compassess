namespace PFLApp.Models
{
    public class PflShipmentModel : PflCustomerModel
    {
        /// <summary>
        ///  Sequence number given to shipment
        /// </summary>
        public int ShipmentSequenceNumber { get; set; }
        /// <summary>
        /// Shipping Method
        /// </summary>
        public string ShippingMethod { get; set; }
    }
}