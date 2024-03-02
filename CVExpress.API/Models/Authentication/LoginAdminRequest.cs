namespace CVExpress.API.Models.Authentication
{
    public class LoginAdminRequest
    {
        #region Login Admin Request Attributes

        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        #endregion
    }
}
