using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PFLApp.Models
{
    /// <summary>
    /// Represents the demographic of a customer
    /// </summary>
    public class PflCustomerModel
    {
        /// <summary>
        /// Unique Customer ID
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// First Name
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string FirstName { get; set; }
        /// <summary>
        /// Last Name
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string LastName { get; set; }
        /// <summary>
        /// Company Name
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string CompanyName { get; set; }
        /// <summary>
        /// Street Address 1
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Address1 { get; set; }
        /// <summary>
        /// Street Address 2
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Address2 { get; set; }
        /// <summary>
        /// City
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string City { get; set; }
        /// <summary>
        /// State
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string State { get; set; }
        /// <summary>
        /// Postal Code
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string PostalCode { get; set; }
        /// <summary>
        /// Country Code
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string CountryCode { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }
        /// <summary>
        /// Phone
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Phone { get; set; }
    }
}