using System;
using System.Linq;
using System.Security.Cryptography;

namespace Account.Application.Services
{
    public class HashService : IHashService
    {
        private const int Iterations = 10000;
        private const int SaltSize = 16;
        private const int KeySize = 32;
        public string Hash(string value)
        {
            using var algorithm = new Rfc2898DeriveBytes(value, SaltSize, Iterations, HashAlgorithmName.SHA512);
            var key = Convert.ToBase64String(algorithm.GetBytes(KeySize));
            var salt = Convert.ToBase64String(algorithm.Salt);
            return $"{Iterations}.{salt}.{key}";
        }

        public bool Compare(string originalValue, string valueToCompare)
        {
            var parts = originalValue.Split('.', 3);
            if (parts.Length != 3)
                throw new FormatException("Unexpected hash format");

            var iterationsToCompare = Convert.ToInt32(parts[0]);
            var saltToCompare = Convert.FromBase64String(parts[1]);
            var keyToCompare = Convert.FromBase64String(parts[2]);

            var algorithm = new Rfc2898DeriveBytes(valueToCompare, saltToCompare, iterationsToCompare, HashAlgorithmName.SHA512);
            var originalKey = algorithm.GetBytes(KeySize);
            return originalKey.SequenceEqual(keyToCompare);
        }
    }
}