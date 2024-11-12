using System.Security.Cryptography;
using System.Text;

namespace NortheastMegabuck;
internal static class Encryption
{
    public static string Key { private get; set; } = string.Empty;

    // This constant is used to determine the keysize of the encryption algorithm in bits.
    // We divide this by 8 within the code below to get the equivalent number of bytes.
    const int _keySize = 256;

    // This constant determines the number of iterations for the password bytes generation function.
    const int _derivationIterations = 100000;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="plainText"></param>
    /// <returns></returns>
    public static string Encrypt(this string? plainText)
    {
        if (string.IsNullOrEmpty(plainText))
        {
            return string.Empty;
        }

        // Salt and IV is randomly generated each time, but is preprended to encrypted cipher text
        // so that the same Salt and IV values can be used when decrypting.
        var saltStringBytes = Generate256BitsOfRandomEntropy();
        var ivStringBytes = Generate256BitsOfRandomEntropy();
        var plainTextBytes = Encoding.UTF8.GetBytes(plainText);

        using var password = new Rfc2898DeriveBytes(Key, saltStringBytes, _derivationIterations, HashAlgorithmName.SHA256);

        var keyBytes = password.GetBytes(_keySize / 8);

        using var symmetricKey = Aes.Create();
        symmetricKey.BlockSize = 128;
        symmetricKey.Mode = CipherMode.CBC;
        symmetricKey.Padding = PaddingMode.PKCS7;

#pragma warning disable CA5401 // Do not use CreateEncryptor with non-default IV
        using var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes.Take(16).ToArray());
#pragma warning restore CA5401 // Do not use CreateEncryptor with non-default IV

        using var memoryStream = new MemoryStream();

        using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
        cryptoStream.FlushFinalBlock();
        // Create the final bytes as a concatenation of the random salt bytes, the random iv bytes and the cipher bytes.
        var cipherTextBytes = saltStringBytes;
        cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
        cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();

        return Convert.ToBase64String(cipherTextBytes);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cipherText"></param>
    /// <returns></returns>
    internal static string Decrypt(this string? cipherText)
    {
        if (string.IsNullOrEmpty(cipherText))
        {
            return string.Empty;
        }

        // Get the complete stream of bytes that represent:
        // [32 bytes of Salt] + [32 bytes of IV] + [n bytes of CipherText]
        var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText);
        // Get the saltbytes by extracting the first 32 bytes from the supplied cipherText bytes.
        var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(_keySize / 8).ToArray();
        // Get the IV bytes by extracting the next 32 bytes from the supplied cipherText bytes.
        var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(_keySize / 8).Take(_keySize / 8).ToArray();
        // Get the actual cipher text bytes by removing the first 64 bytes from the cipherText string.
        var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip(_keySize / 8 * 2).Take(cipherTextBytesWithSaltAndIv.Length - (_keySize / 8 * 2)).ToArray();

        using var password = new Rfc2898DeriveBytes(Key, saltStringBytes, _derivationIterations, HashAlgorithmName.SHA256);

        var keyBytes = password.GetBytes(_keySize / 8);

        using var symmetricKey = Aes.Create();
        symmetricKey.BlockSize = 128;
        symmetricKey.Mode = CipherMode.CBC;
        symmetricKey.Padding = PaddingMode.PKCS7;

        using var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes.Take(16).ToArray());

        using var memoryStream = new MemoryStream(cipherTextBytes);

        using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);

        var plainTextBytes = new byte[cipherTextBytes.Length];
        var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

        return System.Text.Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="encrypted1"></param>
    /// <param name="encrypted2"></param>
    /// <returns></returns>
    public static bool ValuesMatch(string encrypted1, string encrypted2)
        => encrypted1.Decrypt() == encrypted2.Decrypt();

    private static byte[] Generate256BitsOfRandomEntropy()
    {
        var randomBytes = new byte[32]; // 32 Bytes will give us 256 bits.

        using var rngCsp = RandomNumberGenerator.Create();
        // Fill the array with cryptographically secure random bytes.
        rngCsp.GetBytes(randomBytes);
        return randomBytes;
    }
}
