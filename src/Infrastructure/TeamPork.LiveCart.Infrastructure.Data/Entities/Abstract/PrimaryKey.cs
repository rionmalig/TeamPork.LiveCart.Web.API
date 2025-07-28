using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TeamPork.LiveCart.Infrastructure.Data.Entities.Abstract
{
    public class PrimaryKey<Tid>
    {
        [Key, Column(Order = 0)]
        public Tid? Id { get; set; }
    }
}