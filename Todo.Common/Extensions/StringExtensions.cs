using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Todo.Common.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="string"/>.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Computes the hash of the plain text by SHA256 algorithm.
        /// </summary>
        /// <param name="plainText">The string to be computed.</param>
        /// <returns>Hash result string.</returns>
        public static string Sha256(this string plainText)
        {
            using (var algorithm = new SHA256CryptoServiceProvider())
            {
                var textBytes = Encoding.Unicode.GetBytes(plainText);
                var hashBytes = algorithm.ComputeHash(textBytes);

                var hexString = new StringBuilder();
                foreach (var hexByte in hashBytes)
                {
                    hexString.Append(hexByte.ToString("x2"));
                }

                return hexString.ToString();
            }
        }

        /// <summary>
        /// Adds or updates the specified QueryString parameter to the URL string.
        /// </summary>
        /// <param name="url">Url to modify</param>
        /// <param name="name">Query string parameter name to set</param>
        /// <param name="value">Parameter value</param>
        /// <returns>New URL with given parameter set</returns>
        public static string SetQueryStringParameter(this string url, string name, string value)
        {
            if (url == null) url = string.Empty;
            url = url.ToLowerInvariant();

            if (string.IsNullOrWhiteSpace(name)) return url;

            name = name.Trim().ToLowerInvariant();
            value = value.Trim().ToLowerInvariant();

            var queryString = string.Empty;

            if (url.Contains("?"))
            {
                var questionIndex = url.IndexOf("?", StringComparison.Ordinal);
                var startIndex = questionIndex + 1;

                if (startIndex < url.Length)
                {
                    queryString = url.Substring(startIndex);
                }
                url = url.Substring(0, questionIndex);
            }

            var dictionary = new Dictionary<string, string>();

            foreach (var str in queryString.Split('&'))
            {
                if (string.IsNullOrEmpty(str)) continue;

                var strArray = str.Split('=');
                if (strArray.Length == 2)
                {
                    dictionary[strArray[0]] = strArray[1];
                }
            }

            // Adds the specified parameter.
            dictionary[name] = value;

            var builder = new StringBuilder();

            foreach (var str in dictionary.Keys)
            {
                if (builder.Length > 0)
                {
                    builder.Append("&");
                }

                builder.Append(str);
                builder.Append("=");
                builder.Append(dictionary[str]);
            }

            queryString = builder.ToString();

            return url + (string.IsNullOrEmpty(queryString) ? string.Empty : "?" + queryString);
        }
    }
}
