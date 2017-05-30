using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.API.Helpers
{
    public static class StringExtensions
    {
        public static string RowVersionToString(this System.Data.Linq.Binary binary)
        {
            byte[] binarybytes = binary.ToArray();
            string result = "";

            foreach (byte b in binarybytes)
            {
                result += b.ToString();
            }
            return result;
        }

    }
}
