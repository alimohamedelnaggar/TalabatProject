using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order;

namespace Talabat.Repository.Data.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(o=>o.SubTotal).HasColumnType("decimal(18,2)");
            builder.Property(o=>o.Status).HasConversion(os=>os.ToString(),os=>(OrderStatus) Enum.Parse(typeof(OrderStatus),os));

            builder.OwnsOne(o => o.ShippingAddress, sa => sa.WithOwner());

            builder.HasOne(o => o.DeliveryMethod).WithMany().HasForeignKey(o=>o.DeliveryMethodId);
        }
    }
}
