using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PFLApp.Configuration
{
    /// <summary>
    /// Represents configuration of X509 Certificate
    /// </summary>
    public class X509CertConfig
    {
        /// <summary>
        /// Path to certificate file
        /// </summary>
        public string CertFile { get; set; }

        /// <summary>
        /// Validate the configuration
        /// </summary>
        /// <param name="errMsg">Error message representing what was not properly configured</param>
        /// <returns>TRUE if configuration is properly configured</returns>
        public bool IsValid(out string errMsg)
        {
            errMsg = "";
            bool bRet = true;
            if (String.IsNullOrEmpty(CertFile))
            {
                errMsg = "CertFile cannot be empty.";
                bRet = false;
            }
            else if (!File.Exists(CertFile))
            {
                errMsg = "CertFile cannot be located.";
                bRet = false;
            }
            return bRet;
        }
    }
}
