namespace CVExpress.API.Models.Settings
{
    public class JwtSettings
    {
        #region JWT Settings Attributes

        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string Key { get; set; } = string.Empty;

        #endregion
    }
}
