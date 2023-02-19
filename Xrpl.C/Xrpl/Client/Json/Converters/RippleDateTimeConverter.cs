using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Xrpl.Client.Json.Converters
{
    /// <summary> Ripple datetime converter </summary>
    public class RippleDateTimeConverter : DateTimeConverterBase
    {
        /// <summary> ripple start time </summary>
        private static DateTime RippleStartTime = new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        /// <summary>
        /// write  <see cref="DateTime"/>  to json object
        /// </summary>
        /// <param name="writer">writer</param>
        /// <param name="value"> <see cref="DateTime"/> value</param>
        /// <param name="serializer">json serializer</param>
        /// <exception cref="NotSupportedException">value  provided is not a DateTime</exception>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is DateTime date_time)
            {
                long totalSeconds = DateTimeUtils.ISOTimeToRippleTime(date_time);
                writer.WriteValue(totalSeconds);
            }
            else
            {
                throw new ArgumentException("value  provided is not a DateTime", "value");
            }
        }

        /// <summary> read  <see cref="DateTime"/>  from json object </summary>
        /// <param name="reader">json reader</param>
        /// <param name="objectType">object type</param>
        /// <param name="existingValue">object value</param>
        /// <param name="serializer">json serializer</param>
        /// <returns><see cref="DateTime"/></returns>
        /// <exception cref="Exception">Invalid double value. or Invalid token. Expected string</exception>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.Null: return null;
                case JsonToken.String or JsonToken.Integer:
                    {
                        double totalSeconds;
                        try
                        {
                            totalSeconds = Convert.ToDouble(reader.Value, CultureInfo.InvariantCulture);
                            return DateTimeUtils.RippleTimeToISOTime((long)totalSeconds);
                        }
                        catch
                        {
                            throw new Exception("Invalid double value.");
                        }
                        var res = RippleStartTime.AddSeconds(totalSeconds);
                        return res;
                    }
                default: throw new Exception("Invalid token. Expected string");
            }
        }
    }
    public static class DateTimeUtils
    {
        private const int RIPPLE_EPOCH_DIFF = 0x386d4380;

        public static long RippleTimeToUnixTime(long rpepoch)
        {
            return (rpepoch + RIPPLE_EPOCH_DIFF) * 1000;
        }

        public static long UnixTimeToRippleTime(long timestamp)
        {
            return (long)Math.Round((decimal)timestamp / 1000) - RIPPLE_EPOCH_DIFF;
        }

        public static string RippleTimeToISOTimeString(long rippleTime)
        {
            var timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddMilliseconds(RippleTimeToUnixTime(rippleTime)).ToUniversalTime();
            return dateTime.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
        }
        public static DateTime RippleTimeToISOTime(long rippleTime)
        {
            var timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddMilliseconds(RippleTimeToUnixTime(rippleTime)).ToUniversalTime();
            return dateTime;
        }

        public static long ISOTimeToRippleTime(string iso8601)
        {
            var date = DateTime.ParseExact(iso8601, "yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture).ToUniversalTime();
            var milliseconds = (long)date.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
            return UnixTimeToRippleTime(milliseconds);
        }

        public static long ISOTimeToRippleTime(DateTime date)
        {
            var milliseconds = (long)date.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
            return UnixTimeToRippleTime(milliseconds);
        }
    }

}
