using Acme.Interfaces;


namespace Acme.Models.System
{
    public class AuthenticateUserRequest : IAuthenticatUserRequest
    {
        public AuthenticateUserRequest()
        {

        }
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class AuthenticateUserResponse
    {
        public AuthenticateUserResponse() { }
        public Models.User User { get; set; }
        public string AuthToken { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }

}