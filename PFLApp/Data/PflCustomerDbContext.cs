using Microsoft.EntityFrameworkCore;
using PFLApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PFLApp.Data
{
    /// <summary>
    ///  DBContext for PflCustomer
    /// </summary>
    public class PflCustomerDbContext : DbContext
    {
        public PflCustomerDbContext(DbContextOptions<PflCustomerDbContext> options)
            : base(options)
        {
        }

        public DbSet<PflCustomerModel> Customers { get; set; }

    }
}
