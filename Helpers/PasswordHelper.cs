using Microsoft.AspNetCore.Identity;

namespace SafnamBackend.Helpers
{ 
    public static class PasswordHelper
    {
        private static PasswordHasher<string> hasher = new();

        public static string HashPassword(string password)
        {
            return hasher.HashPassword(null, password);
        }

        public static bool VerifyPassword(string password, string hash)
        {
            var result = hasher.VerifyHashedPassword(null, hash, password);
            return result == PasswordVerificationResult.Success;
        }
    }
}
