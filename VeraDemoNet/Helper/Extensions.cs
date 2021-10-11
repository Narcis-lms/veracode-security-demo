using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace VeraDemoNet.Helper
{
    public static class Extensions
    {
        private const int MAX_LOG_LENGTH = 300;
        public static string CleanValue(this string value)
        {
            if (string.IsNullOrEmpty(value)) return "";
            if (value.Length > MAX_LOG_LENGTH) value = value.Substring(0, MAX_LOG_LENGTH);
            value = value.Replace('\n', '_').Replace('\r', '_');
            return WebUtility.HtmlEncode(value);
        }
    }
}