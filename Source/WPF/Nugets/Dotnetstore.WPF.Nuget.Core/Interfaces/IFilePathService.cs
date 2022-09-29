namespace Dotnetstore.WPF.Nuget.Core.Interfaces;

public interface IFilePathService
{
    string ApplicationDataFolder { get; }

    string CommonApplicationDataFolder { get; }

    string CurrentApplicationFolder { get; }

    string? LocalApplicationDataFolder { get; }

    string AddDirectory(string? path);

    string AddDirectory(string? path, string? directoryName);

    void AddFile(string? path);

    string GetCorrectFileName(string? path, string? fileName);
}