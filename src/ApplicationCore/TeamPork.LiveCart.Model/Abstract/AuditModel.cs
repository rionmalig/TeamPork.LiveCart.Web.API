using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamPork.LiveCart.Model.Abstract
{
    public class AuditModel<Tid> : PrimaryKey<Tid>
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
