namespace Dotnetstore.WPF.Intranet.Interfaces;

public interface IApplicationFileService
{
    string? ApplicationLocalFolderPath { get; }

    string? DatabaseFolderPath { get; }

    string? LoggingFolderPath { get; }

    string? SettingsFolderPath { get; }

    string? AppSettingFile { get; }

    string? PersonalSettingFile { get; }

    string ApplicationName { get; }
}