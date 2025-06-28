using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamPork.LiveCart.Model.LiveCart.Auth.Response
{
    public class LoginResponse
    {
        public UserProfile? UserProfile { get; set; }
        public BusinessProfile? BusinessProfile { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public int ExpiresIn { get; set; }
    }
}
