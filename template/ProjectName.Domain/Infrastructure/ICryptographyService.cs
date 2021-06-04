using System.Threading.Tasks;

namespace ProjectName.Application.Domain.Infrastructure
{
    public interface ICryptographyService
    {
        Task<string> Decrypt(string encrypted);
        Task<string> Encrypt(string secret);
    }
}
