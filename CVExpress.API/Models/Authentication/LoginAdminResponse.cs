namespace CVExpress.API.Models.Authentication
{
    public class LoginAdminResponse
    {
        public LoginAdminResponse()
        {
            TokenType = "Bearer";
        }

        public LoginAdminResponse(string accessToken) : this()
        {
            AccessToken = accessToken;
        }

        public string AccessToken { get; set; }
        public string TokenType { get; set; }
    }
}
