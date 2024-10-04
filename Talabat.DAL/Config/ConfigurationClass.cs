using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat
{
    public class ConfigurationClass : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name).IsRequired();
            //builder.ToTable("Product");
            builder.Property(p=>p.Name).HasMaxLength(100);
            builder.Property(p=>p.Description).IsRequired();
            builder.Property(p => p.Price).HasColumnType("decimal(8,2)");
            builder.Property(p => p.PictureUrl).IsRequired();
        }
    }
}
