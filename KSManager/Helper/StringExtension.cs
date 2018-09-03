using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSManager.Helper
{
    public static class StringExtension
    {
        public static string CopyIfNotNull(this string value)
        {
            return value == null ? null : string.Copy(value);
        }
        
    }
}
