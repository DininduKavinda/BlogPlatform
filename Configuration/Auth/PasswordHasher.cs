using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;

namespace TaskManagement.Configuration.Auth
{
    public class PasswordHasher : IPasswordHasher
    {
        private readonly int _iteration = 4;
        private readonly int _memorySize = 1024 * 1024;
        private readonly int _parallelism = 4;

        public string HashPassword(string password)
        {
            byte[] salt = GeneratedSalt(16);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            using var argon2 = new Argon2id(passwordBytes)
            {
                Salt = salt,
                DegreeOfParallelism = _parallelism,
                Iterations = _iteration,
                MemorySize = _memorySize
            };

            byte[] hash = argon2.GetBytes(32);

            return $"{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
        }

        public bool VerifyPassword(string providedPassword, string hashedPassword)
        {
            try
            {
                var parts = hashedPassword.Split('.');
                if (parts.Length != 2) return false;

                byte[] salt = Convert.FromBase64String(parts[0]);
                byte[] expectedHash = Convert.FromBase64String(parts[1]);
                byte[] providedPasswordBytes = Encoding.UTF8.GetBytes(providedPassword);

                using var argon2 = new Argon2id(providedPasswordBytes)
                {
                    Salt = salt,
                    DegreeOfParallelism = _parallelism,
                    Iterations = _iteration,
                    MemorySize = _memorySize
                };

                byte[] actualHash = argon2.GetBytes(32);
                return CryptographicOperations.FixedTimeEquals(expectedHash, actualHash);
            }
            catch
            {
                return false;
            }
        }

        private byte[] GeneratedSalt(int size)
        {
            byte[] salt = new byte[size];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);
            return salt;
        }
    }
}