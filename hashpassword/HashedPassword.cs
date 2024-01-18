using BC = BCrypt.Net.BCrypt;
using System.Security.Cryptography;
using System.Text;

namespace password.hashedpassword;

public class HashedPassword {
    private byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);

    public string hashedpassword(string password) {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password,  BC.GenerateSalt(13));
            return passwordHash;
    }

    public bool Decode(string encodedPassword ,string password)
    {
        return BC.Verify(password, encodedPassword);
    }
}