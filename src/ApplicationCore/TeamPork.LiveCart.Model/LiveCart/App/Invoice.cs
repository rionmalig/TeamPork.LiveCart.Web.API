using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamPork.LiveCart.Model.Abstract;

namespace TeamPork.LiveCart.Model.LiveCart.App
{
    public class Invoice : SyncedModel<long>
    {
        public required string InvoiceTitle { get; set; }
        public required DateOnly OrderedAt { get; set; }
        public required DateOnly DueAt { get; set; }
        public float Total { get; set; }
        public required string Status { get; set; }
        public long? CustomerId { get; set; }
        public string? CustomerClientId { get; set; }
    }
}
