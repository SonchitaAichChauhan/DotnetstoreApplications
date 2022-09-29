namespace Dotnetstore.WPF.Intranet.Models;

public class Config
{
    public string? CurrentVersion { get; set; }

    public string? Culture { get; set; }

    public API API { get; set; }
}

public class API
{
    public DotnetstoreIntranet? DotnetstoreIntranet { get; set; }
}

public class DotnetstoreIntranet
{
    public string? HttpDotnetstoreWebAPIClientName { get; set; }

    public string? HttpDotnetstoreWebAPIClientBaseAddress { get; set; }
}