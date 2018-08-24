using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KSManager_API.Helper
{
    public static class StringExtensions
    {
        public static byte[] ToHex(this string hex)
        {
            return Enumerable.Range(0, hex.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                .ToArray();
        }
    }
}
