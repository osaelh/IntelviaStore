using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Entities.Models
{
    public class ProductVariant
    {
        [JsonIgnore]
        public Product Product { get; set; }
        public int ProductId { get; set; }

        public Edition Edition { get; set; }
        public int EditionId { get; set; }
        public decimal Price { get; set; }
        public decimal OriginalPrice { get; set; }

    }
}
