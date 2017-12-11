using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PFLApp.Services;
using PFLApp.Models;
using Microsoft.AspNetCore.Authorization;
using PFLApp.Data;
using System.Security.Claims;

namespace PFLApp.Controllers
{
    /// <summary>
    /// Controller for Placing and Viewing Orders
    /// </summary>
    public class OrdersController : Controller
    {
        private readonly IPflApiSvc _pflApiSvc;
        private readonly ILogger<OrdersController> _logger;
        private readonly PflCustomerDbContext _pflCustomerDbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">Logger</param>
        /// <param name="pflApiSvc">PFL Api Service</param>
        public OrdersController(ILogger<OrdersController> logger, IPflApiSvc pflApiSvc, PflCustomerDbContext pflCustomerDbContext)
        {
            _pflApiSvc = pflApiSvc;
            _logger = logger;
            _pflCustomerDbContext = pflCustomerDbContext;
        }

        /// <summary>
        /// Method for Placing New Orders
        /// </summary>
        /// <param name="items">Items to Order</param>
        /// <returns>Status code 200 if the order was placed successfully</returns>
        [HttpPost]
        [Route("/api/order")]
        [ProducesResponseType(typeof(PflOrderModel), 200)]
        [Authorize]
        public async Task<IActionResult> PlaceOrder([FromBody] List<PflOrderItemModel> items)
        {
            try
            {
                _logger.LogDebug("Attempting to place order");

                if (items == null || items.Count == 0)
                {
                    _logger.LogError("Placing an Order requires that there is at least one Item to be placed");
                    return new BadRequestObjectResult("Missing list of order items");
                }

                // Grab Customer Information from DB
                PflCustomerModel customer = GetCustomerFromIdentity();
                if (customer == null)
                {
                    _logger.LogError("Customer not defined.  Order cannot be placed.");
                    return new BadRequestObjectResult("Customer cannot be found. Order will not be placed");
                }
                PflShipmentModel shipment = new PflShipmentModel()
                {
                    ShipmentSequenceNumber = 1,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Address1 = customer.Address1,
                    City = customer.City,
                    CompanyName = customer.CompanyName,
                    CountryCode = customer.CountryCode,
                    Email = customer.Email,
                    Phone = customer.Phone,
                    PostalCode = customer.PostalCode,
                    State = customer.State
                };
                PflOrderModel order = new PflOrderModel()
                {
                    Items = items,
                    OrderCustomer = customer,
                    Shipments = new List<PflShipmentModel>()
                    {
                        shipment
                    }
                };
                var orderNumber = await _pflApiSvc.PlaceOrderAsync(order);

                if (String.IsNullOrEmpty(orderNumber))
                {
                    string msg = "Order could not be placed successfully.";
                    _logger.LogError(msg);
                    return new BadRequestObjectResult(msg);
                }
                else
                {
                    order.OrderNumber = orderNumber;
                }

                _logger.LogInformation("Successfully ordered {count} items.  Order number is [{orderNumber}]", items.Count, orderNumber);
                return new OkObjectResult(order);
            }
            catch (Exception e)
            {
                _logger.LogError("Unable to process order due to exception.  Message: {message};  StackTrace: {stacktrace}", e.Message, e.StackTrace);
                return new BadRequestObjectResult("Unable to process order. " + e.Message);
            }
            finally
            {
                _logger.LogDebug("Finished attempt to place order");
            }
        }

        private PflCustomerModel GetCustomerFromIdentity()
        {
            var email = GetEmailFromCurrentIdentity(this.User);
            if (String.IsNullOrEmpty(email))
            {
                _logger.LogError("Unable to locate Email of logged on user.");
                return null;
            }
            return _pflCustomerDbContext.Customers.First(a => a.Email == email);
        }

        private string GetEmailFromCurrentIdentity(ClaimsPrincipal user)
        {
            if (user.Identity.IsAuthenticated)
            {
                return user.Claims.FirstOrDefault(v => v.Type == ClaimTypes.Name).Value;
            }
            return null;
        }

        /// <summary>
        /// Returns the list of products offered by PFL
        /// </summary>
        /// <returns>List of products</returns>
        [HttpGet]
        [Route("api/products")]
        [ProducesResponseType(typeof(List<PflProductModel>), 200)]
        [Authorize]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                var products = await _pflApiSvc.GetProductsAsync();
                if (products == null)
                {
                    throw new Exception("Problem with PFL Api Service.");
                }
                return new OkObjectResult(products);
            }
            catch (Exception e)
            {
                _logger.LogError("Unable to retrieve list of products.  Message: {message}; StackTrace: {stacktrace}", e.Message, e.StackTrace);
                return new BadRequestObjectResult("Unable to retrieve product list. " + e.Message);
            }
        }
    }
}