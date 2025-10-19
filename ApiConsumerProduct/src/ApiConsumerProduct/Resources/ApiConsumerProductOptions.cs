namespace ApiConsumerProduct.Resources;

public class ApiConsumerProductOptions
{
    public const string SectionName = "ApiConsumerProduct";
    
    public RabbitMqOptions RabbitMq { get; set; } = new RabbitMqOptions();
    public ConnectionStringOptions ConnectionStrings { get; set; } = new ConnectionStringOptions();
    public AuthOptions Auth { get; set; } = new AuthOptions();
    public string JaegerHost { get; set; } = String.Empty;
    
    public class RabbitMqOptions
    {
        public const string SectionName = $"{ApiConsumerProductOptions.SectionName}:RabbitMq";
        public const string HostKey = nameof(Host);
        public const string VirtualHostKey = nameof(VirtualHost);
        public const string UsernameKey = nameof(Username);
        public const string PasswordKey = nameof(Password);
        public const string PortKey = nameof(Port);

        public string Host { get; set; } = String.Empty;
        public string VirtualHost { get; set; } = String.Empty;
        public string Username { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
        public string Port { get; set; } = String.Empty;
    }

    public class ConnectionStringOptions
    {
        public const string SectionName = $"{ApiConsumerProductOptions.SectionName}:ConnectionStrings";
        public const string ApiConsumerProductKey = nameof(ApiConsumerProduct); 
            
        public string ApiConsumerProduct { get; set; } = String.Empty;
    }
    
    
    public class AuthOptions
    {
        public const string SectionName = $"{ApiConsumerProductOptions.SectionName}:Auth";

        public string Audience { get; set; } = String.Empty;
        public string Authority { get; set; } = String.Empty;
        public string AuthorizationUrl { get; set; } = String.Empty;
        public string TokenUrl { get; set; } = String.Empty;
        public string ClientId { get; set; } = String.Empty;
        public string ClientSecret { get; set; } = String.Empty;
    }
}

public static class ApiConsumerProductOptionsExtensions
{
    public static ApiConsumerProductOptions GetApiConsumerProductOptions(this IConfiguration configuration)
    {
        return configuration
            .GetSection(ApiConsumerProductOptions.SectionName)
            .Get<ApiConsumerProductOptions>();
    }
    
    public static ApiConsumerProductOptions.RabbitMqOptions GetRabbitMqOptions(this IConfiguration configuration)
    {
        return configuration
            .GetSection(ApiConsumerProductOptions.RabbitMqOptions.SectionName)
            .Get<ApiConsumerProductOptions.RabbitMqOptions>();
    }
    
    public static ApiConsumerProductOptions.ConnectionStringOptions GetConnectionStringOptions(this IConfiguration configuration)
    {
        return configuration
            .GetSection(ApiConsumerProductOptions.ConnectionStringOptions.SectionName)
            .Get<ApiConsumerProductOptions.ConnectionStringOptions>();
    }
    
    public static ApiConsumerProductOptions.AuthOptions GetAuthOptions(this IConfiguration configuration)
    {
        return configuration
            .GetSection(ApiConsumerProductOptions.AuthOptions.SectionName)
            .Get<ApiConsumerProductOptions.AuthOptions>();
    }

    public static string GetJaegerHostValue(this IConfiguration configuration)
    {
        return configuration
            .GetSection(ApiConsumerProductOptions.SectionName)
            .GetSection(nameof(ApiConsumerProductOptions.JaegerHost)).Value;
    }
}