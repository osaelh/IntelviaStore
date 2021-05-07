using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTO
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public int EditionId { get; set; }
        public string ProductTitle { get; set; }
        public string EditionName { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
    }
}
