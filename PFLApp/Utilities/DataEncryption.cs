using Microsoft.Extensions.Logging;
using PFLApp.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PFLApp.Utilities
{
    /// <summary>
    /// Represents utility methods for encrypting and decrypting values based on an X509 certificate
    /// </summary>
    public class DataEncryption
    {
        // NOTE: For the sake of simplifying demonstration...using a hard-coded password to open up the cert.  
        // Normally, this password would be removed in favor of a cert configuration such that it can only be opened by certain domain accounts that are privileged to run this service
        private static readonly string cCertPwd = "pfl";

        /// <summary>
        /// Encrypts the value and returns a Base64 encoded representation of the encryption blob
        /// </summary>
        /// <param name="logger">Logger</param>
        /// <param name="x509CertConfig">X509 Certificate Configuration</param>
        /// <param name="clearTextValue">Value to encrypt</param>
        /// <returns>Base64 encoded encrypted result</returns>
        public static string EncryptData(ILogger logger, X509CertConfig x509CertConfig, string clearTextValue)
        {
            /// Validate Arguments in public method
            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }
            if (x509CertConfig == null)
            {
                throw new ArgumentNullException("x509CertConfig");
            }
            if (String.IsNullOrEmpty(clearTextValue))
            {
                // No encryption work needs to be done
                return clearTextValue;
            }

            try
            {
                logger.LogDebug("Attempt to encrypt value");
                using (X509Certificate2 cert = new X509Certificate2(x509CertConfig.CertFile, cCertPwd))
                {
                    if (cert == null)
                    {
                        throw new Exception("Unable to create X509Certificate2 using cert file " + x509CertConfig.CertFile);
                    }
                    using (RSA rsa = cert.GetRSAPublicKey())
                    {
                        byte[] valueBytes = Encoding.UTF8.GetBytes(clearTextValue);
                        return Convert.ToBase64String(rsa.Encrypt(valueBytes,RSAEncryptionPadding.OaepSHA512));
                    }
                }
            }
            catch (Exception e)
            {
                logger.LogError("Unable to perform encryption of the value using the X509 Certificate at the location: [{certFile}]. Reason: {reason}", x509CertConfig.CertFile, e.Message);
                throw new Exception("Unable to encrypt using certificate "+x509CertConfig.CertFile, e);
            }
            finally
            {
                logger.LogDebug("Finished attempt to encrypt value.");
            }
        }

        /// <summary>
        /// Decrypts an base64 encoded encryption string
        /// </summary>
        /// <param name="logger">Logger</param>
        /// <param name="x509CertConfig">X509 Certificate Configuration</param>
        /// <param name="encryptedValue">Base64 encoded value to decrypt</param>
        /// <returns>Cleartext Value</returns>
        public static string DecryptData(ILogger logger, X509CertConfig x509CertConfig, string encryptedValue)
        {
            /// Validate Arguments in public method
            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }
            if (x509CertConfig == null)
            {
                throw new ArgumentNullException("x509CertConfig");
            }
            if (String.IsNullOrEmpty(encryptedValue))
            {
                // No decryption work needs to be done
                return encryptedValue;
            }

            try
            {
                logger.LogDebug("Attempt to decrypt value");
                using (X509Certificate2 cert = new X509Certificate2(x509CertConfig.CertFile, cCertPwd))
                {
                    if (cert == null)
                    {
                        throw new Exception("Unable to create X509Certificate2 using cert file " + x509CertConfig.CertFile);
                    }
                    using (RSA rsa = cert.GetRSAPrivateKey())
                    {
                        byte[] valueBytes = Convert.FromBase64String(encryptedValue);
                        return Encoding.UTF8.GetString(rsa.Decrypt(valueBytes,RSAEncryptionPadding.OaepSHA512));
                    }
                }
            }
            catch (Exception e)
            {
                logger.LogError("Unable to perform decryption of the value using the X509 Certificate at the location: [{certFile}]. Reason: {reason}", x509CertConfig.CertFile, e.Message);
                throw new Exception("Unable to decrypt using certificate " + x509CertConfig.CertFile, e);
            }
            finally
            {
                logger.LogDebug("Finished attempt to decrypt value.");
            }
        }
    }
}
