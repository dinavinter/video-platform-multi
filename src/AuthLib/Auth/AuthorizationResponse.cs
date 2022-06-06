using System;

namespace Auth
{
    public class AuthorizationResponse
    {
        public string message { get; set; }
        public string jwtBearer { get; set; }
        public bool success { get; set; }
    }
}