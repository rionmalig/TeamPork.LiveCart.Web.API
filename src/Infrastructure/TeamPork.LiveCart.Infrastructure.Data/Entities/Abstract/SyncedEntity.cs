using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamPork.LiveCart.Infrastructure.Data.Entities.Abstract
{
    public class SyncedEntity<Tid> : PrimaryKey<Tid>
    {
        [Column(Order = 1)]
        public string? ClientId { get; set; }
        public required DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
