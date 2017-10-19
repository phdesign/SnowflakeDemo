using System;
using System.Linq;
using Flakey;

namespace SnowflakeDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var generator = new IdGenerator(0);
            for (var i = 0; i < 100; i++)
            {
                var id = generator.CreateId();
                Console.WriteLine($"Base10: {id}, Encoded: {Encode(id)}, Decodes Correctly: {Decode(Encode(id)) == id}");
            }
        }

        public static string Encode(long uid)
        {
            string encoded = Convert.ToBase64String(BitConverter.GetBytes(uid));
            encoded = encoded
                .Replace("/", "_")
                .Replace("+", "-");
            return encoded.Substring(0, 11);
        }

        public static long Decode(string encoded)
        {
            encoded = encoded.Replace("_", "/");
            encoded = encoded.Replace("-", "+");
            byte[] buffer = Convert.FromBase64String(encoded + "=");
            return BitConverter.ToInt64(buffer, 0);
        }
    }
}
