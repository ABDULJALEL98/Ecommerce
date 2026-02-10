using Ecom.Core.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.Data.Config
{
    public class PhotoConfiguration : IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder.HasData(new Photo
            {
                Id = 1,
                ImageName = "product-1.jpg",
                ProductId = 1
            },
            new Photo
            {
                Id = 2,
                ImageName = "product-2.jpg",
                ProductId = 2
            },
            new Photo {
                Id = 3,
                ImageName = "product-3.jpg",
                ProductId = 3
            }
            );
        }
    }
}
