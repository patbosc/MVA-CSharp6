using System;
using System.Threading.Tasks;

internal class Repository
{
    internal static Task<Repository> OpenAsync(string path)
    {
        throw new NotImplementedException();
    }

    internal Task<String> ReadAsync()
    {
        throw new NotImplementedException();
    }

    internal Task LogAsync(RepositoryException e)
    {
        throw new NotImplementedException();
    }

    internal Task CloseAsync()
    {
        throw new NotImplementedException();
    }
}