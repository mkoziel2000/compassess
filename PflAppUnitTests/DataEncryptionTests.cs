using Microsoft.Extensions.Logging;
using Moq;
using PFLApp.Configuration;
using PFLApp.Utilities;
using System;
using Xunit;

namespace PflAppUnitTests
{
    public class DataEncryptionTests
    {
        [Theory]
        [InlineData(null, null)]
        [InlineData("", null)]
        [InlineData("", "")]
        public void TestEncryption_Negative(string certFile, string value)
        {
            Mock<ILogger> mockLogger = new Mock<ILogger>();
            X509CertConfig certConfig = new X509CertConfig()
            {
                CertFile = certFile
            };

            // Encryption Methods
            Assert.Throws<ArgumentNullException>(() => DataEncryption.EncryptData(null, null, null));
            Assert.Throws<ArgumentNullException>(() => DataEncryption.EncryptData(mockLogger.Object, null, value));

            // Null or empty string in should be same coming out
            Assert.Equal(value, DataEncryption.EncryptData(mockLogger.Object, certConfig, value));


            // Decryption Methods
            Assert.Throws<ArgumentNullException>(() => DataEncryption.DecryptData(null, null, null));
            Assert.Throws<ArgumentNullException>(() => DataEncryption.DecryptData(mockLogger.Object, null, value));

            // Null or empty string in should result in same coming out
            Assert.Equal(value, DataEncryption.DecryptData(mockLogger.Object, certConfig, value));
        }

        [Theory]
        [InlineData("Pr!nt123")]
        [InlineData("136085")]
        [InlineData("   aaa~!@#$%^&*()_+-={}|[]\\:\";'<>?,./zzz   ")]
        public void TestEncryption_Positive(string value)
        {
            Mock<ILogger> mockLogger = new Mock<ILogger>();
            X509CertConfig certConfig = new X509CertConfig()
            {
                CertFile = "pfl.pfx"
            };

            var encVal = DataEncryption.EncryptData(mockLogger.Object, certConfig, value);
            Assert.NotNull(encVal);
            Assert.NotEqual(value, encVal);  // Verifies that we are working with an encrypted string here
            Assert.True(encVal.Length > 0);

            var result = DataEncryption.DecryptData(mockLogger.Object, certConfig, encVal);
            Assert.NotNull(result);
            Assert.Equal(value, result);
        }

        [Fact]
        public void TestEncryption_BadCert()
        {
            Mock<ILogger> mockLogger = new Mock<ILogger>();
            X509CertConfig certConfig = new X509CertConfig()
            {
                CertFile = "certnotfound.pfx"
            };

            Assert.Throws<Exception>(() => DataEncryption.EncryptData(mockLogger.Object, certConfig, "Encrypt Me"));
            Assert.Throws<Exception>(() => DataEncryption.EncryptData(mockLogger.Object, certConfig, "Decrypt Me"));
        }
    }
}
