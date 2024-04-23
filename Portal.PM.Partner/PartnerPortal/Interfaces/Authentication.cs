namespace Acme.Interfaces
{
    interface IAuthenticatUserRequest
    {
        string Username { get; set; }
        string Password { get; set; }
    }
}