using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Paylike.NET.Utilities
{
    public class Descriptor
    {
        public static string Format(string Descriptor)
        {
            Descriptor = Regex.Replace(Descriptor, @"[^\u0000-\u007F]+", string.Empty);

            if (Descriptor.Length > 22)
            {
                Descriptor = Descriptor.Substring(0, 22);
            }

            return Descriptor;
        }
    }
}
