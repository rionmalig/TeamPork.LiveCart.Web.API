using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamPork.LiveCart.Core.Services.Helper.Interface
{
    public interface IPasswordHasherService
    {
        public string Hash(string value);
        public bool Verify(string value, string hash);
    }
}
