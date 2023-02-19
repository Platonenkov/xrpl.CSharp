using System.Text.RegularExpressions;

using Newtonsoft.Json;

using Xrpl.Client.Extensions;

namespace Xrpl.Client.Models.Common
{
    public partial class Currency
    {
        [JsonIgnore]
        public string CurrencyValidName => CurrencyCode is { Length: > 0 } row 
            ? row.Length > 3 
                ? IsHexCurrencyCode(row) 
                    ? row.FromHexString().Trim('\0') 
                    : row 
                : row 
            : string.Empty;
        /// <summary>
        /// check currency code for HEX 
        /// </summary>
        /// <param name="code">currency code</param>
        /// <returns></returns>
        public static bool IsHexCurrencyCode(string code) => Regex.IsMatch(code, @"[0-9a-fA-F]{40}", RegexOptions.IgnoreCase);


    }
}

