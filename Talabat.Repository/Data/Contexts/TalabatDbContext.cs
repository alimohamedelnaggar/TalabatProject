﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Repository.Data.Contexts
{
   public class TalabatDbContext:DbContext
    {
        public TalabatDbContext(DbContextOptions<TalabatDbContext> options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        

       public DbSet<Product> Products { get; set; }
       public DbSet<ProductBrand> ProductBrands { get; set; }
       public DbSet<ProductCategory> ProductCategories { get; set; }
    }
}
