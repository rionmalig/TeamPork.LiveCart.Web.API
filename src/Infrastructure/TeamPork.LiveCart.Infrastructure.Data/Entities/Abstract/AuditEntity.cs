using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamPork.LiveCart.Infrastructure.Data.Entities.Abstract
{
    public class AuditEntity<Tid> : PrimaryKey<Tid>
    {
        public required bool Active { get; set; }
        public required int CreatedBy { get; set; }
        public required DateOnly CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateOnly? ModifiedDate { get; set; }
        public int? DeletedBy { get; set; }
        public DateOnly? DeletedDate { get; set; }
    }
}
