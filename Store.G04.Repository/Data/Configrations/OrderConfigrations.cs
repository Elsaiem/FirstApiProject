using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Store.G04.Core.Entities;
using Store.G04.Core.Entities.Orderr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Repository.Data.Configrations
{
    public class OrderConfigrations : IEntityTypeConfiguration<Order>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Order> builder)
        {
            builder.Property(O => O.SubTotal).HasColumnType("decimal(18,2)");

            builder.Property(O=>O.Status)
                    .HasConversion(O=>O.ToString(),Ostatus=>(OrderStatus)Enum.Parse(typeof(OrderStatus),Ostatus));


            builder.OwnsOne(O => O.ShipingAddress, Sa => Sa.WithOwner());


            builder.HasOne(o=>o.DeliveryMethod).WithMany().HasForeignKey(o=>o.DeliveryMethodId);

        }
    }
}
