using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace NewFlower.Models
{
    public class DataContext : DbContext
    {
        public DataContext()
            : base("DbConnection")
        { }

        public DbSet<Measurements> Measurements { get; set; }
    }
}