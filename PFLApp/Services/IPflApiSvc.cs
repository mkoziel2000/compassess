using PFLApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PFLApp.Services
{
    /// <summary>
    /// Interface representing PFL Services
    /// </summary>
    public interface IPflApiSvc
    {
        Task<List<PflProductModel>> GetProductsAsync();
        Task<string> PlaceOrderAsync(PflOrderModel order);
    }
}
