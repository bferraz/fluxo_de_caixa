using System;

namespace API.Identidade.Models
{
    public class UserResponseLoginModel
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
    }
}
