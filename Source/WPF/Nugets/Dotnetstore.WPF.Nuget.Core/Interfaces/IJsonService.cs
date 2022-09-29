namespace Dotnetstore.WPF.Nuget.Core.Interfaces;

public interface IJsonService
{
    Task SaveAsync<T>(string file, T entity);

    void Save<T>(string file, T entity);

    Task<T?> GetAsync<T>(string file);

    T? Get<T>(string file);
}