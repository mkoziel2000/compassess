using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PFLApp.Models
{
    /// <summary>
    /// Represents a Product Order
    /// </summary>
    public class PflOrderModel
    {
        /// <summary>
        /// Order Number assigned by PFL when order is placed
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string OrderNumber { get; set; }
        /// <summary>
        /// Partner Order Reference
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string PartnerOrderReference { get; set; }
        /// <summary>
        /// Customer Demographic
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public PflCustomerModel OrderCustomer { get; set; }
        /// <summary>
        /// List of products being ordered
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<PflOrderItemModel> Items { get; set; }
        /// <summary>
        /// List of Shipment definitions
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<PflShipmentModel> Shipments { get; set; }
        /// <summary>
        /// List of Item to Shipments mappings
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<PflItemShipmentModel> ItemShipments { get; set; }

        /// <summary>
        /// Validate that the Order structure has all the required elements
        /// </summary>
        /// <param name="errMsg">Error message about what is not valid with the order</param>
        /// <returns>TRUE if the Order Model is Valid</returns>
        public bool IsValid(out string errMsg)
        {
            bool bRet = true;
            errMsg = "";
            if (OrderCustomer == null || String.IsNullOrEmpty(OrderCustomer.FirstName) ||
                String.IsNullOrEmpty(OrderCustomer.LastName) || String.IsNullOrEmpty(OrderCustomer.Address1) ||
                String.IsNullOrEmpty(OrderCustomer.City) || String.IsNullOrEmpty(OrderCustomer.PostalCode) ||
                String.IsNullOrEmpty(OrderCustomer.CountryCode) || String.IsNullOrEmpty(OrderCustomer.Phone))
            {
                errMsg = "OrderCustomer is missing at least one required field";
                bRet = false;
            }
            else if (Items == null || Items.Count == 0)
            {
                errMsg = "Order Items not defined";
                bRet = false;
            }
            else if (Items.TrueForAll(item =>
                item.ItemSequenceNumber > 0 &&
                item.ProductId > 0 &&
                item.Quantity > 0))
            {
                errMsg = "At least one item is not properly defined.";
                bRet = false;
            }
            
            //TODO: Do more validation once API requirements are better understood

            return bRet;
        }
    }
}
