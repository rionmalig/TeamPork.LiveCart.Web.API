using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamPork.LiveCart.Model.Abstract;

namespace TeamPork.LiveCart.Model.LiveCart.App
{
    public class Customer : SyncedModel<long>
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? Email { get; set; }
        public string? ContactNumber { get; set; }
        public string? TaxRegNo { get; set; }
        public string? Note { get; set; }
    }
}
