using System.Security.Cryptography;
using System.Text;
using meeplematch_api.Utils;

namespace meeplematch_api.Security;

public static class Encryption
{
    /// <summary>
    /// Generateds random byte array of specified size in bytes.
    /// </summary>
    /// <remarks>Default length value is 16</remarks>
    /// <param name="length">value size in bytes</param>
    /// <returns>byte[]</returns>
    public static byte[] GenerateRandomBytes(int length = 16)
    {
        var bytes = RandomNumberGenerator.GetBytes(length);
        return bytes;
    }

    /// <summary>
    /// Converts input string to byte array.
    /// </summary>
    /// <param name="input">input string</param>
    /// <returns>byte[]</returns>
    public static byte[] ConvertStringToBytes(string input)
    {
        return Encoding.UTF8.GetBytes(input);
    }

    /// <summary>
    /// Derives and encrypts given string using Pbkdf2 algorithm. Method provided multiple overrides.
    /// </summary>
    /// <param name="password"></param>
    /// <param name="salt"></param>
    /// <returns>KeyValuePair of byte arrays</returns>
    public static KeyValuePair<byte[], byte[]> Encrypt(string password)
    {
        var salt = GenerateRandomBytes();
        var encryptedPassword = Rfc2898DeriveBytes.Pbkdf2(
            ConvertStringToBytes(password),
            salt,
            Constants.IterationCount,
            Constants.HashAlgorithm,
            Constants.HashSize);

        return new KeyValuePair<byte[], byte[]>(encryptedPassword, salt);
    }
    
    
    /// <summary>
    /// Derives and encrypts given string using Pbkdf2 algorithm and specified salt.
    /// </summary>
    /// <param name="password"></param>
    /// <param name="salt"></param>
    /// <returns>KeyValuePair of byte arrays</returns>
    public static KeyValuePair<byte[], byte[]> Encrypt(string password, byte[] salt)
    {
        var encryptedPassword = Rfc2898DeriveBytes.Pbkdf2(
            ConvertStringToBytes(password),
            salt,
            Constants.IterationCount,
            Constants.HashAlgorithm,
            Constants.HashSize);

        return new KeyValuePair<byte[], byte[]>(encryptedPassword, salt);
    }
}