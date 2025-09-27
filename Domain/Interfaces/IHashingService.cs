using System.Security;

namespace Domain.Interfaces;

public interface IHashingService
{
    public string Hash(string password);
}