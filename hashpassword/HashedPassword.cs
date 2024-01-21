using BC = BCrypt.Net.BCrypt;

namespace password.hashedpassword;

public class HashedPassword
{
    public string hashedpassword(string password)
    {
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(password, BC.GenerateSalt(13));
        return passwordHash;
    }

    public bool Decode(string encodedPassword, string password)
    {
        return BC.Verify(password, encodedPassword);
    }
}