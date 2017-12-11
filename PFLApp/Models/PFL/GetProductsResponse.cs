using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PFLApp.Models.PFL
{
    /// <summary>
    /// Represents the Response of a Get Products Call
    /// </summary>
    public class GetProductsResponse : ApiResults
    {
        private List<PflProductModel> _Data;

        /// <summary>
        /// List of Products
        /// </summary>
        public List<PflProductModel> Data {
            get
            {
                if (_Data == null)
                {
                    // Look at generic container for Data Element containing a List<PflProductModel> structure
                    if (Results.AdditionalElements != null && Results.AdditionalElements["data"] != null)
                        _Data = Results.AdditionalElements["data"].ToObject<List<PflProductModel>>();
                    else
                        _Data = new List<PflProductModel>();
                }
                return _Data;
            }
            set
            {
                _Data = value;
            }
        } 
    }
}
