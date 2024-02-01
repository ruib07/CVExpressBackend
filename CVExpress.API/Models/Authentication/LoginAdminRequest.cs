namespace CVExpress.API.Models.Authentication
{
    public class LoginAdminRequest
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
