namespace Note.API.Services.Authentication.Access
{
    public class AuthOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int TokenLifetime { get; set; }
        public string Key { get; set; }

    }
}
