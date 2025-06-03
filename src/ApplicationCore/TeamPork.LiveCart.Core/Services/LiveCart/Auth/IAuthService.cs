using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamPork.LiveCart.Model.LiveCart.Auth.Request;
using TeamPork.LiveCart.Model.LiveCart.Auth.Response;

namespace TeamPork.LiveCart.Core.Services.LiveCart.Auth
{
    public interface IAuthService
    {
        public Task<LoginResponse?> Register(RegisterRequest registerRequest);
    }
}
