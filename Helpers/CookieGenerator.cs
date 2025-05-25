using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ABSOLUTE_CINEMA.Helpers
{
    public static class CookieGenerator
    {
        private const string SaltData = "QADLz4qk3rVgBSGjDfAH3XWVqKKagMXezBPv7TmXvwnXDDepHaLBv4JnTGRwLg9tzbmV778DUEAEa6JPv66hy7SwHBL4z4FbGdh2MVs4kq9RcaZEAszuP5ccLsEfqCpwdSvVVt479DCZrwjSHrJVwaja9WQaWAmEY9NsPvEHKnFwHTGAvPXpjpCxkbedYquEauLvZLphwmJLUteZ4QAXU6Z4F3PDmh3wsQXvSctQBHvNWf";
        private static readonly byte[] Salt = Encoding.ASCII.GetBytes(SaltData);
        private const string Secret = "BjXNmq5MKKaraLwxz9uaATvFwE4Rj679KguTRE8c2j56FnkuKJKfkGbZEeDGFDvsGYNHpUXFUUUuUHBR4UV3T2kumguhubg6Gpt7CyqGDbUPrMvPc67kX3yP";

        public static string Create(string value)
        {
            if (string.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(value));
            using (var aes = new RijndaelManaged())
            {
                var key = new Rfc2898DeriveBytes(Secret, Salt);
                aes.Key = key.GetBytes(aes.KeySize / 8);
                var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (var ms = new MemoryStream())
                {
                    ms.Write(BitConverter.GetBytes(aes.IV.Length), 0, sizeof(int));
                    ms.Write(aes.IV, 0, aes.IV.Length);
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    using (var sw = new StreamWriter(cs))
                        sw.Write(value);
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        public static string Validate(string value)
        {
            if (string.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(value));
            var data = Convert.FromBase64String(value);

            using (var ms = new MemoryStream(data))
            {
                using (var aes = new RijndaelManaged())
                {
                    var key = new Rfc2898DeriveBytes(Secret, Salt);
                    aes.Key = key.GetBytes(aes.KeySize / 8);
                    aes.IV = ReadByteArray(ms);
                    var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                    using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    using (var sr = new StreamReader(cs))
                        return sr.ReadToEnd();
                }
            }
        }

        private static byte[] ReadByteArray(Stream s)
        {
            var len = new byte[sizeof(int)];
            if (s.Read(len, 0, len.Length) != len.Length)
                throw new Exception("Bad format");
            var buffer = new byte[BitConverter.ToInt32(len, 0)];
            if (s.Read(buffer, 0, buffer.Length) != buffer.Length)
                throw new Exception("Bad format");
            return buffer;
        }
    }
}
