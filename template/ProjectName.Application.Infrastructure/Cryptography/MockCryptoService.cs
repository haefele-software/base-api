using System.Threading.Tasks;
using ProjectName.Application.Domain.Infrastructure;

namespace ProjectName.Application.Infrastructure.Cryptography
{
    public class MockCryptoService : ICryptographyService
    {
        public async Task<string> Decrypt(string encrypted)
        {
            return await Task.FromResult($"unencrypted_{ encrypted }").ConfigureAwait(false);
        }

        public async Task<string> Encrypt(string secret)
        {
            return await Task.FromResult($"encrypted_{ secret }").ConfigureAwait(false);
        }
    }
}
