using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamPork.LiveCart.Model.Abstract;

namespace TeamPork.LiveCart.Model.LiveCart.App
{
    public class Item : SyncedModel<long>
    {
        public required string Name { get; set; }
        public string? Code { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public float Discount { get; set; }
        public bool IsPercentage { get; set; }
    }
}
