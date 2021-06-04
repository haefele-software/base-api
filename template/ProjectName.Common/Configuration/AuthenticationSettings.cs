namespace ProjectName.Common.Configuration
{
    public sealed class AuthenticationSettings
    {
        public string Audience { get; set; }

        public string Authority { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string Domain { get; set; }
    }
}
