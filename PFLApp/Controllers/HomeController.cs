using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PFLApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using PFLApp.Services;

namespace PFLApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPflApiSvc _pflApiSvc;

        public HomeController(ILogger<HomeController> logger, IPflApiSvc pflApiSvc)
        {
            _logger = logger ?? throw new ArgumentNullException("logger");
            _pflApiSvc = pflApiSvc ?? throw new ArgumentNullException("pflApiSvc");
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            try
            {
                var products = await _pflApiSvc.GetProductsAsync();
                if (products == null)
                {
                    throw new Exception("Problem with PFL Api Service.");
                }
                return View(products);
            }
            catch (Exception e)
            {
                _logger.LogError("Unable to retrieve list of products.  Message: {message}; StackTrace: {stacktrace}", e.Message, e.StackTrace);
                return new BadRequestObjectResult("Unable to retrieve product list. " + e.Message);
            }
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
