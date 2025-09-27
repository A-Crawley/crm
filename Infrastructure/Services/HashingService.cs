using System.Security;
using System.Security.Cryptography;
using System.Text;
using Domain.Interfaces;

namespace Infrastructure.Services;

public class HashingService : IHashingService
{
    private readonly string _salt = "asodkok123123";
    private readonly string _pepper = "asdasd";
    
    public string Hash(string password)
    {
        var byteArray = SHA256.HashData(Season(password));
        return Convert.ToBase64String(byteArray);
    }

    private byte[] Season(string data)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append(_salt);
        stringBuilder.Append(data);
        stringBuilder.Append(_pepper);
        return Encoding.UTF8.GetBytes(stringBuilder.ToString());
    }
}