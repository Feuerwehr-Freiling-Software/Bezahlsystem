using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Paymentsystem.Shared.Extensions
{
    public static class Hashing
    {
        public static string ToSha256(this string input)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                var hash = BitConverter.ToString(bytes).Replace("-", "").ToLowerInvariant();
                return hash;
            }
        }
    }
}
