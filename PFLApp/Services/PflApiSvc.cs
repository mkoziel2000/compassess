using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PFLApp.Configuration;
using PFLApp.Models;
using PFLApp.Models.PFL;
using PFLApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PFLApp.Services
{
    /// <summary>
    /// Represents PFL Api Services
    /// </summary>
    public class PflApiSvc : IPflApiSvc
    {
        private readonly ILogger<PflApiSvc> _logger;
        private readonly PflLinkConfig _pflLinkConfig;
        private readonly X509CertConfig _x509CertConfig;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">Logger</param>
        /// <param name="pflLinkConfig">PFL Link Configuration</param>
        /// <param name="x509CertConfig">X509 Certificate Configuration for Sensitive Data decryption</param>
        public PflApiSvc(ILogger<PflApiSvc> logger, IOptionsSnapshot<PflLinkConfig> pflLinkConfig, IOptionsSnapshot<X509CertConfig> x509CertConfig)
        {
            _logger = logger ?? throw new ArgumentNullException("logger");
            _pflLinkConfig = pflLinkConfig.Value ?? throw new ArgumentNullException("pflLinkConfig");
            _x509CertConfig = x509CertConfig.Value ?? throw new ArgumentNullException("x509CertConfig");
        }

        /// <summary>
        /// Retrieve the Products
        /// </summary>
        /// <returns>List of Products Retrieved</returns>
        public async Task<List<PflProductModel>> GetProductsAsync()
        {
            try
            {
                _logger.LogDebug("Attempting to get products from PFL.");
                Uri pflUri = new Uri(new Uri(_pflLinkConfig.Url), "/products?apikey=" + Uri.EscapeUriString(DataEncryption.DecryptData(_logger, _x509CertConfig, _pflLinkConfig.ApiKey)));
                var response = await CallApiAsync(pflUri, HttpMethod.Get, null);

                if (response != null)
                {
                    var products = JsonConvert.DeserializeObject<GetProductsResponse>(response);
                    if (products.Meta.StatusCode != 200)
                    {
                        _logger.LogError("Error ststus returned from PFL Api. Details: {errors}", products.Results.Errors);
                        return null;
                    }
                    else
                    {
                        _logger.LogInformation("Retrieve [{count}] products from PFL.", products.Data.Count);
                        return products.Data;
                    }
                }
                else
                {
                    throw new Exception("Something bad happened.  Did not receive response during Products GET call.");
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Exception while retrieving list of products from PFL. Reason: {reason}", e.Message);
                throw e;
            }
            finally
            {
                _logger.LogDebug("Finished attempting to get products from PFL.");
            }
        }

        /// <summary>
        /// Place an Order with the PFL Apis and return an Order Number
        /// </summary>
        /// <param name="order">Order to be placed</param>
        /// <returns>Order number that was placed successfully</returns>
        public async Task<string> PlaceOrderAsync(PflOrderModel order)
        {
            if (order == null)
            {
                throw new ArgumentNullException("order");
            }

            try
            {
                _logger.LogDebug("Attempting to place order into PFL.");
                Uri pflUri = new Uri(new Uri(_pflLinkConfig.Url), "/orders?apikey=" + Uri.EscapeUriString(DataEncryption.DecryptData(_logger, _x509CertConfig, _pflLinkConfig.ApiKey)));
                var response = await CallApiAsync(pflUri, HttpMethod.Post, JsonConvert.SerializeObject(order));

                if (response != null)
                {
                    var orderResp = JsonConvert.DeserializeObject<PflOrderResponse>(response);
                    if (orderResp.Meta.StatusCode != 200)
                    {
                        _logger.LogError("Error ststus returned from PFL Api. Details: {errors}", orderResp.Results.Errors);
                        return null;
                    }
                    else
                    {
                        _logger.LogInformation("Order #{order} placed with PFL", orderResp.Data.OrderNumber);
                        return orderResp.Data.OrderNumber;
                    }
                }
                else
                {
                    throw new Exception("Something bad happened.  Did not receive response during Product Order call.");
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Exception while placing order with PFL. Reason: {reason}", e.Message);
                throw e;
            }
            finally
            {
                _logger.LogDebug("Finished attempting to place order into PFL.");
            }
        }

        private async Task<string> CallApiAsync(Uri uri, HttpMethod method, string jsonPayload)
        {
            using (HttpClient client = new HttpClient())
            {
                // Apply Authorization Header
                string token = Convert.ToBase64String(Encoding.UTF8.GetBytes(_pflLinkConfig.Username + ":" + DataEncryption.DecryptData(_logger, _x509CertConfig, _pflLinkConfig.Password)));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", token);

                using (HttpRequestMessage reqMsg = new HttpRequestMessage(method, uri))
                {
                    if (method == HttpMethod.Post)
                    {
                        reqMsg.Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                    }

                    var resp = await client.SendAsync(reqMsg);
                    if (!resp.IsSuccessStatusCode)
                    {
                        _logger.LogError("Failed to call PFL Api using Uri [{uri}]. Status Code: {statusCode}. Reason: {reason}", uri, resp.StatusCode, resp.ReasonPhrase);
                        if (resp.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                        {
                            throw new UnauthorizedAccessException("Not Authorized to Access PFL Apis.");
                        }
                        else
                        {
                            _logger.LogError("Received an Error from PFL Apis for Uri [{uri}]", uri);
                            return await resp.Content.ReadAsStringAsync();
                        }
                    }
                    else
                    {
                        _logger.LogInformation("PFL Api successfully called at uri [{uri}].", uri);
                        return await resp.Content.ReadAsStringAsync();
                    }
                }
            }
        }
    }
}
