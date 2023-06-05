namespace Api
{
    public class AppSettings
    {
        public DbSettings Db { get; set; }
        public JwtSettings Jwt { get; set; }
    }

    public class DbSettings
    {
        public string ConnectionString { get; set; }
    }

    public class JwtSettings
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
