using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamPork.LiveCart.Model.Abstract;

namespace TeamPork.LiveCart.Model.LiveCart.Auth
{
    public class UserRefreshToken : PrimaryKey<long>
    {
        public long UserSeqId { get; set; }
        public string? Token { get; set; }
        public DateTime? Expirey { get; set; }
    }
}
