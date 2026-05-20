namespace Identity.Contracts.Respsone
{
    public class AuthResponse
    {
        public string AccessToken { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
    }
}
