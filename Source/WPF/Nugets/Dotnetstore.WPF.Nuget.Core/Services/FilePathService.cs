using Dotnetstore.WPF.Nuget.Core.Abstracts;
using Dotnetstore.WPF.Nuget.Core.Interfaces;

namespace Dotnetstore.WPF.Nuget.Core.Services;

public sealed class FilePathService : Disposable, IFilePathService
{
    string IFilePathService.ApplicationDataFolder => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

    string IFilePathService.CommonApplicationDataFolder => Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);

    string IFilePathService.CurrentApplicationFolder => AppDomain.CurrentDomain.BaseDirectory;

    string IFilePathService.LocalApplicationDataFolder => Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

    string IFilePathService.AddDirectory(string? path, string? directoryName)
    {
        if (string.IsNullOrWhiteSpace(path) ||
            string.IsNullOrWhiteSpace(directoryName))
        {
            return string.Empty;
        }

        var fullName = Path.Combine(path, directoryName);

        if (!Directory.Exists(fullName))
        {
            Directory.CreateDirectory(fullName);
        }

        return fullName;
    }

    string IFilePathService.AddDirectory(string? path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return string.Empty;
        }

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        return path;
    }

    void IFilePathService.AddFile(string? path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return;
        }

        if (!File.Exists(path))
        {
            using var stream = File.Create(path);
        }
    }

    string IFilePathService.GetCorrectFileName(string? path, string? fileName)
    {
        if (string.IsNullOrWhiteSpace(path) ||
            string.IsNullOrWhiteSpace(fileName))
        {
            return string.Empty;
        }

        var (fileExist, fileNameWithoutExtension, fileExtension) = CheckIfFileExistAndSplitFileName(path, fileName);

        if (!fileExist)
        {
            return string.Empty;
        }

        return ChangeFileNameUntilNonExistingFileIsFound(path, fileNameWithoutExtension, fileExtension);
    }

    private static string ChangeFileNameUntilNonExistingFileIsFound(string path, string fileNameWithoutExtension, string fileExtension)
    {
        var index = 1;

        while (true)
        {
            var fileNameEnd = $"({index})";
            var fileExist = FileExists(path, $"{fileNameWithoutExtension}{fileNameEnd}", fileExtension);

            if (!fileExist)
            {
                return Path.Combine(path, $"{fileNameWithoutExtension}{fileNameEnd}", fileExtension);
            }

            index++;
        }
    }

    private static (bool fileExist, string fileNameWithoutExtension, string fileExtension) CheckIfFileExistAndSplitFileName(string path, string fileName)
    {
        var fileExtension = Path.GetExtension(fileName);
        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
        var fileExists = FileExists(path, fileNameWithoutExtension, fileExtension);
        return (fileExists, fileNameWithoutExtension, fileExtension);
    }

    private static bool FileExists(string path, string fileName, string extension)
    {
        return File.Exists(Path.Combine(path, fileName, extension));
    }
}