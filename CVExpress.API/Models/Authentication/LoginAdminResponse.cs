namespace CVExpress.API.Models.Authentication
{
    public class LoginAdminResponse
    {
        #region Login Admin Constructors

        public LoginAdminResponse()
        {
            TokenType = "Bearer";
        }

        public LoginAdminResponse(string accessToken) : this()
        {
            AccessToken = accessToken;
        }

        #endregion

        #region Login Admin Response Attributes

        public string AccessToken { get; set; }
        public string TokenType { get; set; }

        #endregion
    }
}
