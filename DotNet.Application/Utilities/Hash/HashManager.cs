using Microsoft.AspNetCore.Identity;

namespace DotNet.Application.Utilities.Hash
{
    public static class HashManager
    {
        public static string ConvertToHash(string _string)
        {
            HashedString hashedString = new();
            PasswordHasher<HashedString> stringHasher = new();
            hashedString.ChangeString(stringHasher.HashPassword(hashedString, _string));
            string truncatedHash = hashedString.String.Substring(0, Math.Min(hashedString.String.Length, 60));
            return truncatedHash;
        }

        public static bool ValidadeHash(string hash, string _string)
        {
            HashedString hashedString = new();
            PasswordHasher<HashedString> stringHasher = new();
            var status = stringHasher.VerifyHashedPassword(hashedString, hash, _string);

            return status switch
            {
                PasswordVerificationResult.Failed => false,
                PasswordVerificationResult.Success => true,
                PasswordVerificationResult.SuccessRehashNeeded => true,
                _ => throw new InvalidOperationException(),
            };
        }
    }
}