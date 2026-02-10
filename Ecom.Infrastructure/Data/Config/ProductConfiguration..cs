using Ecom.Core.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecom.Infrastructure.Data.Config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Name)
                   .IsRequired();

            builder.Property(x => x.Description)
                   .IsRequired();

            builder.Property(x => x.NewPrice)
                   .HasColumnType("decimal(18,2)");
            builder.HasData(
                
                
                
                new Product
                {
                    Id = 1,
                    Name = "Product 1",
                    Description = "Description for Product 1",
                   NewPrice = 10.99m,
                    CategoryId = 1
                },
                new Product
                {
                    Id = 2,
                    Name = "Product 2",
                    Description = "Description for Product 2",
                    NewPrice = 19.99m,
                    CategoryId = 2
                },
                new Product
                {
                    Id = 3,
                    Name = "Product 3",
                    Description = "Description for Product 3",
                    NewPrice = 5.99m,
                    CategoryId = 1
                }
            );
        }
    }
}
