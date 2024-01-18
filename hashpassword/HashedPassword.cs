using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace password.hashedpassword;

public class HashedPassword {
    private byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);

    public string hashedpassword(string password) {
        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
        password: password!,
        salt: salt,
        prf: KeyDerivationPrf.HMACSHA256,
        iterationCount: 100000,
        numBytesRequested: 256 / 8));
        return hashed;
    }
}