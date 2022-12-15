using Newtonsoft.Json;

using System.Globalization;

namespace Xrpl.Client.Models.Common
{
    public partial class Currency
    {
        public Currency()
        {
            CurrencyCode = "XRP";
        }

        [JsonProperty("currency")]
        public string CurrencyCode { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("issuer")]
        public string Issuer { get; set; }

        [JsonIgnore]
        public decimal ValueAsNumber
        {
            get => string.IsNullOrEmpty(Value)
                ? 0
                : decimal.Parse(Value,
                    NumberStyles.AllowLeadingSign
                    | (NumberStyles.AllowLeadingSign & NumberStyles.AllowDecimalPoint)
                    | (NumberStyles.AllowLeadingSign & NumberStyles.AllowExponent)
                    | (NumberStyles.AllowLeadingSign & NumberStyles.AllowExponent & NumberStyles.AllowDecimalPoint)
                    | (NumberStyles.AllowExponent & NumberStyles.AllowDecimalPoint)
                    | NumberStyles.AllowExponent
                    | NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
            set => Value = value.ToString(CurrencyCode == "XRP" ? "G0" : "G15", CultureInfo.InvariantCulture);
        }

        [JsonIgnore]
        public decimal? ValueAsXrp
        {
            get
            {
                if (CurrencyCode != "XRP" || string.IsNullOrEmpty(Value))
                    return null;
                return ValueAsNumber / 1000000;
            }
            set
            {
                if (value.HasValue)
                {
                    CurrencyCode = "XRP";
                    decimal val = value.Value * 1000000;
                    Value = val.ToString("G0", CultureInfo.InvariantCulture);
                }
                else
                {
                    Value = "0";
                }
            }
        }

        #region Overrides of Object

        public override string ToString() => CurrencyValidName == "XRP" ? $"XRP: {ValueAsXrp:0.######}" : $"{CurrencyValidName}: {ValueAsNumber:0.###############}";

        #endregion
    }
}
