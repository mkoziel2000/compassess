using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PFLApp.Configuration
{
    /// <summary>
    /// Provides the Configuration for communication with PFL Ordering system
    /// </summary>
    public class PflLinkConfig
    {
        /// <summary>
        /// Url of the PFL Ordering system
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// Username of service account
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// Password of Service Account
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Api Key for representing authorized access to PFL Ordering system
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// Validates that the Configuration is properly configured
        /// </summary>
        /// <param name="errMsg">Error message representing what was not properly configured</param>
        /// <returns>TRUE if configuration is properly configured</returns>
        public bool IsValid(out string errMsg)
        {
            errMsg = "";
            bool bRet = true;
            if (String.IsNullOrEmpty(Url))
            {
                errMsg = "Url cannot be empty.";
                bRet = false;
            }
            else if (String.IsNullOrEmpty(Username))
            {
                errMsg = "Username cannot be empty.";
                bRet = false;
            }
            else if (String.IsNullOrEmpty(Password))
            {
                errMsg = "Password cannot be empty.";
                bRet = false;
            }
            else if (String.IsNullOrEmpty(ApiKey))
            {
                errMsg = "ApiKey cannot be empty.";
                bRet = false;
            }
            return bRet;
        }
    }
}
