namespace ReadingIsGood.Models;

public class AppSettings
{
    public LoggingSettings Logging { get; set; }
    public ConnectionStringsSettings ConnectionStrings { get; set; }
    public JwtSettings JwtSettings { get; set; }
    public string AllowedHosts { get; set; }
}

public class LoggingSettings
{
    public LogLevelSettings LogLevel { get; set; }
}

public class LogLevelSettings
{
    public string Default { get; set; }
    public string MicrosoftAspNetCore { get; set; }
}

public class ConnectionStringsSettings
{
    public string ReadingIsGoodConnection { get; set; }
}

public class JwtSettings
{
    public string Key { get; set; }
    public string Issuer { get; set; }
}