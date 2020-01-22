namespace Account.Application.Configurations
{
    public class TokenConfiguration
    {
        public string Secret { get; set; }
        public int Expiration { get; set; }
    }
}
