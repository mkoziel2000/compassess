using PFLApp.Models.PFL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PFLApp.Models
{
    public class PflOrderResponse : ApiResults
    {
        private PflOrderModel _Data;

        /// <summary>
        /// Order returned from PFL including Order Number
        /// </summary>
        public PflOrderModel Data
        {
            get
            {
                if (_Data == null)
                {
                    // Look at generic container for Data Element containing a List<PflProductModel> structure
                    if (Results.AdditionalElements != null && Results.AdditionalElements["data"] != null)
                        _Data = Results.AdditionalElements["data"].ToObject<PflOrderModel>();
                    else
                        _Data = new PflOrderModel();
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
