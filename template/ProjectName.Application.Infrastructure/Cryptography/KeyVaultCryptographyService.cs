using Azure.Identity;
using Azure.Security.KeyVault.Keys.Cryptography;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProjectName.Application.Domain.Infrastructure;
using ProjectName.Common.Configuration;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ProjectName.Application.Infrastructure.Cryptography
{
    public sealed class KeyVaultCryptographyService : ICryptographyService
    {
        private readonly ILogger<KeyVaultCryptographyService> _logger;
        private readonly CryptographyClient _cryptographyClient;
        private const int KeySize = 2048 / 8 - 42;

        public KeyVaultCryptographyService(ILogger<KeyVaultCryptographyService> logger, IOptions<CryptographySettings> cryptographyOptions)
        {
            this._logger = logger;
            var config = cryptographyOptions.Value;
            _cryptographyClient = new CryptographyClient(config.KeyUri, new DefaultAzureCredential(config.InteractiveLoginEnabled));
        }

        public async Task<string> Decrypt(string encrypted)
        {
            try
            {
                var decrypted = await _cryptographyClient.DecryptAsync(EncryptionAlgorithm.RsaOaep256, Convert.FromBase64String(encrypted)).ConfigureAwait(false);
                return Encoding.UTF8.GetString(decrypted.Plaintext);
            }
            catch (Exception e)
            {
                _logger.LogError("Decryption failed", e);
                throw;
            }

        }
        /// <summary>
        /// Encrypts a secret, if the bytes of the string exceeds the key length InvalidDataException will be thrown
        /// </summary>
        /// <param name="secret"></param>
        /// <returns></returns>
        public async Task<string> Encrypt(string secret)
        {
            var secretBytes = Encoding.UTF8.GetBytes(secret);
            if (secretBytes.Length >= KeySize)
            {
                throw new InvalidDataException("Data exceeds encryption algorithm allowable length");
            }

            try
            {
                var encrypted = await _cryptographyClient.EncryptAsync(EncryptionAlgorithm.RsaOaep256, secretBytes).ConfigureAwait(false);
                return Convert.ToBase64String(encrypted.Ciphertext);
            }
            catch (Exception e)
            {
                _logger.LogError("Encryption failed", e);
                throw;
            }

        }
    }
}
