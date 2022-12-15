using Newtonsoft.Json;

using System;
using System.Linq;
using System.Text;
using Org.BouncyCastle.Math;
using Xrpl.Client.Exceptions;

namespace Ripple.Binary.Codec.Util
{
    internal static class Utils
    {
        public static byte[] ZeroPad(byte[] arr, int len)
        {
            if (arr.Length > len)
            {
                throw new InvalidOperationException(
                    $"Existing length of {arr.Length} > " +
                    $"desired length of {len}");
            }
            if (arr.Length == len)
            {
                return arr;
            }
            var extended = new byte[len];
            var startingAt = len - arr.Length;
            Array.Copy(arr, 0, extended, startingAt, arr.Length);
            return extended;
        }
    }

    /// <summary>
    ///     Converts base data types to an array of bytes, and an array of bytes to base
    ///     data types.
    ///     All info taken from the meta data of System.BitConverter. This implementation
    ///     allows for Endianness consideration.
    ///</summary>
    public static class Bits
    {
        /// <summary>
        ///     Indicates the byte order ("endianess") in which data is stored in this computer
        ///     architecture.
        ///</summary>
        /// 
        public static bool IsLittleEndian { get; set; } = false;

        /// <summary>
        ///     Converts the specified double-precision floating point number to a 64-bit
        ///     signed integer.
        ///
        /// Parameters:
        ///   value:
        ///     The number to convert.
        ///
        /// Returns:
        ///     A 64-bit signed integer whose value is equivalent to value.
        ///</summary>
        public static long DoubleToInt64Bits(double value) { throw new NotImplementedException(); }
        ///
        /// <summary>
        ///     Returns the specified Boolean value as an array of bytes.
        ///
        /// Parameters:
        ///   value:
        ///     A Boolean value.
        ///
        /// Returns:
        ///     An array of bytes with length 1.
        ///</summary>
        public static byte[] GetBytes(bool value)
        {
            if (IsLittleEndian)
            {
                return BitConverter.GetBytes(value);
            }
            return BitConverter.GetBytes(value).Reverse().ToArray();
        }

        public static byte[] GetBytes(byte value)
        {
            return new[] { value };
        }

        ///
        /// <summary>
        ///     Returns the specified Unicode character value as an array of bytes.
        ///
        /// Parameters:
        ///   value:
        ///     A character to convert.
        ///
        /// Returns:
        ///     An array of bytes with length 2.
        ///</summary>
        public static byte[] GetBytes(char value)
        {
            if (IsLittleEndian)
            {
                return BitConverter.GetBytes(value);
            }
            return BitConverter.GetBytes(value).Reverse().ToArray();
        }

        ///
        /// <summary>
        ///     Returns the specified double-precision floating point value as an array of
        ///     bytes.
        ///
        /// Parameters:
        ///   value:
        ///     The number to convert.
        ///
        /// Returns:
        ///     An array of bytes with length 8.
        ///</summary>
        public static byte[] GetBytes(double value)
        {
            if (IsLittleEndian)
            {
                return BitConverter.GetBytes(value);
            }
            return BitConverter.GetBytes(value).Reverse().ToArray();
        }

        ///
        /// <summary>
        ///     Returns the specified single-precision floating point value as an array of
        ///     bytes.
        ///
        /// Parameters:
        ///   value:
        ///     The number to convert.
        ///
        /// Returns:
        ///     An array of bytes with length 4.
        ///</summary>
        public static byte[] GetBytes(float value)
        {
            if (IsLittleEndian)
            {
                return BitConverter.GetBytes(value);
            }
            return BitConverter.GetBytes(value).Reverse().ToArray();
        }

        ///
        /// <summary>
        ///     Returns the specified 32-bit signed integer value as an array of bytes.
        ///
        /// Parameters:
        ///   value:
        ///     The number to convert.
        ///
        /// Returns:
        ///     An array of bytes with length 4.
        ///</summary>
        public static byte[] GetBytes(int value)
        {
            if (IsLittleEndian)
            {
                return BitConverter.GetBytes(value);
            }
            return BitConverter.GetBytes(value).Reverse().ToArray();
        }

        ///
        /// <summary>
        ///     Returns the specified 64-bit signed integer value as an array of bytes.
        ///
        /// Parameters:
        ///   value:
        ///     The number to convert.
        ///
        /// Returns:
        ///     An array of bytes with length 8.
        ///</summary>
        public static byte[] GetBytes(long value)
        {
            if (IsLittleEndian)
            {
                return BitConverter.GetBytes(value);
            }
            return BitConverter.GetBytes(value).Reverse().ToArray();
        }

        ///
        /// <summary>
        ///     Returns the specified 16-bit signed integer value as an array of bytes.
        ///
        /// Parameters:
        ///   value:
        ///     The number to convert.
        ///
        /// Returns:
        ///     An array of bytes with length 2.
        ///</summary>
        public static byte[] GetBytes(short value)
        {
            if (IsLittleEndian)
            {
                return BitConverter.GetBytes(value);
            }
            return BitConverter.GetBytes(value).Reverse().ToArray();
        }

        ///
        /// <summary>
        ///     Returns the specified 32-bit unsigned integer value as an array of bytes.
        ///
        /// Parameters:
        ///   value:
        ///     The number to convert.
        ///
        /// Returns:
        ///     An array of bytes with length 4.
        ///</summary>

        public static byte[] GetBytes(uint value)
        {
            if (IsLittleEndian)
            {
                return BitConverter.GetBytes(value);
            }
            return BitConverter.GetBytes(value).Reverse().ToArray();
        }

        ///
        /// <summary>
        ///     Returns the specified 64-bit unsigned integer value as an array of bytes.
        ///
        /// Parameters:
        ///   value:
        ///     The number to convert.
        ///
        /// Returns:
        ///     An array of bytes with length 8.
        ///</summary>

        public static byte[] GetBytes(ulong value)
        {
            if (IsLittleEndian)
            {
                return BitConverter.GetBytes(value);
            }
            return BitConverter.GetBytes(value).Reverse().ToArray();
        }

        ///
        /// <summary>
        ///     Returns the specified 16-bit unsigned integer value as an array of bytes.
        ///
        /// Parameters:
        ///   value:
        ///     The number to convert.
        ///
        /// Returns:
        ///     An array of bytes with length 2.
        ///</summary>
        public static byte[] GetBytes(ushort value)
        {
            if (IsLittleEndian)
            {
                return BitConverter.GetBytes(value);
            }
            return BitConverter.GetBytes(value).Reverse().ToArray();
        }

        ///
        /// <summary>
        ///     Converts the specified 64-bit signed integer to a double-precision floating
        ///     point number.
        ///
        /// Parameters:
        ///   value:
        ///     The number to convert.
        ///
        /// Returns:
        ///     A double-precision floating point number whose value is equivalent to value.
        ///</summary>
        public static double Int64BitsToDouble(long value) { throw new NotImplementedException(); }
        ///
        /// <summary>
        ///     Returns a Boolean value converted from one byte at a specified position in
        ///     a byte array.
        ///
        /// Parameters:
        ///   value:
        ///     An array of bytes.
        ///
        ///   startIndex:
        ///     The starting position within value.
        ///
        /// Returns:
        ///     true if the byte at startIndex in value is nonzero; otherwise, false.
        ///
        /// Exceptions:
        ///   System.ArgumentNullException:
        ///     value is null.
        ///
        ///   System.ArgumentOutOfRangeException:
        ///     startIndex is less than zero or greater than the length of value minus 1.
        ///</summary>
        public static bool ToBoolean(byte[] value, int startIndex) { throw new NotImplementedException(); }
        ///
        /// <summary>
        ///     Returns a Unicode character converted from two bytes at a specified position
        ///     in a byte array.
        ///
        /// Parameters:
        ///   value:
        ///     An array.
        ///
        ///   startIndex:
        ///     The starting position within value.
        ///
        /// Returns:
        ///     A character formed by two bytes beginning at startIndex.
        ///
        /// Exceptions:
        ///   System.ArgumentException:
        ///     startIndex equals the length of value minus 1.
        ///
        ///   System.ArgumentNullException:
        ///     value is null.
        ///
        ///   System.ArgumentOutOfRangeException:
        ///     startIndex is less than zero or greater than the length of value minus 1.
        ///</summary>
        public static char ToChar(byte[] value, int startIndex) { throw new NotImplementedException(); }
        ///
        /// <summary>
        ///     Returns a double-precision floating point number converted from eight bytes
        ///     at a specified position in a byte array.
        ///
        /// Parameters:
        ///   value:
        ///     An array of bytes.
        ///
        ///   startIndex:
        ///     The starting position within value.
        ///
        /// Returns:
        ///     A double precision floating point number formed by eight bytes beginning
        ///     at startIndex.
        ///
        /// Exceptions:
        ///   System.ArgumentException:
        ///     startIndex is greater than or equal to the length of value minus 7, and is
        ///     less than or equal to the length of value minus 1.
        ///
        ///   System.ArgumentNullException:
        ///     value is null.
        ///
        ///   System.ArgumentOutOfRangeException:
        ///     startIndex is less than zero or greater than the length of value minus 1.
        ///</summary>
        public static double ToDouble(byte[] value, int startIndex) { throw new NotImplementedException(); }
        ///
        /// <summary>
        ///     Returns a 16-bit signed integer converted from two bytes at a specified position
        ///     in a byte array.
        ///
        /// Parameters:
        ///   value:
        ///     An array of bytes.
        ///
        ///   startIndex:
        ///     The starting position within value.
        ///
        /// Returns:
        ///     A 16-bit signed integer formed by two bytes beginning at startIndex.
        ///
        /// Exceptions:
        ///   System.ArgumentException:
        ///     startIndex equals the length of value minus 1.
        ///
        ///   System.ArgumentNullException:
        ///     value is null.
        ///
        ///   System.ArgumentOutOfRangeException:
        ///     startIndex is less than zero or greater than the length of value minus 1.
        ///</summary>
        public static short ToInt16(byte[] value, int startIndex)
        {
            if (IsLittleEndian)
            {
                return BitConverter.ToInt16(value, startIndex);
            }
            return BitConverter.ToInt16(value.Reverse().ToArray(), value.Length - sizeof(Int16) - startIndex);
        }

        ///
        /// <summary>
        ///     Returns a 32-bit signed integer converted from four bytes at a specified
        ///     position in a byte array.
        ///
        /// Parameters:
        ///   value:
        ///     An array of bytes.
        ///
        ///   startIndex:
        ///     The starting position within value.
        ///
        /// Returns:
        ///     A 32-bit signed integer formed by four bytes beginning at startIndex.
        ///
        /// Exceptions:
        ///   System.ArgumentException:
        ///     startIndex is greater than or equal to the length of value minus 3, and is
        ///     less than or equal to the length of value minus 1.
        ///
        ///   System.ArgumentNullException:
        ///     value is null.
        ///
        ///   System.ArgumentOutOfRangeException:
        ///     startIndex is less than zero or greater than the length of value minus 1.
        ///</summary>
        public static int ToInt32(byte[] value, int startIndex)
        {
            if (IsLittleEndian)
            {
                return BitConverter.ToInt32(value, startIndex);
            }
            return BitConverter.ToInt32(value.Reverse().ToArray(), value.Length - sizeof(Int32) - startIndex);
        }

        ///
        /// <summary>
        ///     Returns a 64-bit signed integer converted from eight bytes at a specified
        ///     position in a byte array.
        ///
        /// Parameters:
        ///   value:
        ///     An array of bytes.
        ///
        ///   startIndex:
        ///     The starting position within value.
        ///
        /// Returns:
        ///     A 64-bit signed integer formed by eight bytes beginning at startIndex.
        ///
        /// Exceptions:
        ///   System.ArgumentException:
        ///     startIndex is greater than or equal to the length of value minus 7, and is
        ///     less than or equal to the length of value minus 1.
        ///
        ///   System.ArgumentNullException:
        ///     value is null.
        ///
        ///   System.ArgumentOutOfRangeException:
        ///     startIndex is less than zero or greater than the length of value minus 1.
        ///</summary>
        public static long ToInt64(byte[] value, int startIndex)
        {
            if (IsLittleEndian)
            {
                return BitConverter.ToInt64(value, startIndex);
            }
            return BitConverter.ToInt64(value.Reverse().ToArray(), value.Length - sizeof(Int64) - startIndex);
        }

        ///
        /// <summary>
        ///     Returns a single-precision floating point number converted from four bytes
        ///     at a specified position in a byte array.
        ///
        /// Parameters:
        ///   value:
        ///     An array of bytes.
        ///
        ///   startIndex:
        ///     The starting position within value.
        ///
        /// Returns:
        ///     A single-precision floating point number formed by four bytes beginning at
        ///     startIndex.
        ///
        /// Exceptions:
        ///   System.ArgumentException:
        ///     startIndex is greater than or equal to the length of value minus 3, and is
        ///     less than or equal to the length of value minus 1.
        ///
        ///   System.ArgumentNullException:
        ///     value is null.
        ///
        ///   System.ArgumentOutOfRangeException:
        ///     startIndex is less than zero or greater than the length of value minus 1.
        ///</summary>
        public static float ToSingle(byte[] value, int startIndex)
        {
            if (IsLittleEndian)
            {
                return BitConverter.ToSingle(value, startIndex);
            }
            return BitConverter.ToSingle(value.Reverse().ToArray(), value.Length - sizeof(Single) - startIndex);
        }

        ///
        /// <summary>
        ///     Converts the numeric value of each element of a specified array of bytes
        ///     to its equivalent hexadecimal string representation.
        ///
        /// Parameters:
        ///   value:
        ///     An array of bytes.
        ///
        /// Returns:
        ///     A System.String of hexadecimal pairs separated by hyphens, where each pair
        ///     represents the corresponding element in value; for example, "7F-2C-4A".
        ///
        /// Exceptions:
        ///   System.ArgumentNullException:
        ///     value is null.
        ///</summary>
        public static string ToString(byte[] value)
        {
            if (IsLittleEndian)
            {
                return BitConverter.ToString(value);
            }
            return BitConverter.ToString(value.Reverse().ToArray());
        }

        ///
        /// <summary>
        ///     Converts the numeric value of each element of a specified subarray of bytes
        ///     to its equivalent hexadecimal string representation.
        ///
        /// Parameters:
        ///   value:
        ///     An array of bytes.
        ///
        ///   startIndex:
        ///     The starting position within value.
        ///
        /// Returns:
        ///     A System.String of hexadecimal pairs separated by hyphens, where each pair
        ///     represents the corresponding element in a subarray of value; for example,
        ///     "7F-2C-4A".
        ///
        /// Exceptions:
        ///   System.ArgumentNullException:
        ///     value is null.
        ///
        ///   System.ArgumentOutOfRangeException:
        ///     startIndex is less than zero or greater than the length of value minus 1.
        ///</summary>
        public static string ToString(byte[] value, int startIndex)
        {
            if (IsLittleEndian)
            {
                return BitConverter.ToString(value, startIndex);
            }
            return BitConverter.ToString(value.Reverse().ToArray(), startIndex);
        }

        ///
        /// <summary>
        ///     Converts the numeric value of each element of a specified subarray of bytes
        ///     to its equivalent hexadecimal string representation.
        ///
        /// Parameters:
        ///   value:
        ///     An array of bytes.
        ///
        ///   startIndex:
        ///     The starting position within value.
        ///
        ///   length:
        ///     The number of array elements in value to convert.
        ///
        /// Returns:
        ///     A System.String of hexadecimal pairs separated by hyphens, where each pair
        ///     represents the corresponding element in a subarray of value; for example,
        ///     "7F-2C-4A".
        ///
        /// Exceptions:
        ///   System.ArgumentNullException:
        ///     value is null.
        ///
        ///   System.ArgumentOutOfRangeException:
        ///     startIndex or length is less than zero.  -or- startIndex is greater than
        ///     zero and is greater than or equal to the length of value.
        ///
        ///   System.ArgumentException:
        ///     The combination of startIndex and length does not specify a position within
        ///     value; that is, the startIndex parameter is greater than the length of value
        ///     minus the length parameter.
        ///</summary>
        public static string ToString(byte[] value, int startIndex, int length)
        {
            if (IsLittleEndian)
            {
                return BitConverter.ToString(value, startIndex, length);
            }
            return BitConverter.ToString(value.Reverse().ToArray(), startIndex, length);
        }

        ///
        /// <summary>
        ///     Returns a 16-bit unsigned integer converted from two bytes at a specified
        ///     position in a byte array.
        ///
        /// Parameters:
        ///   value:
        ///     The array of bytes.
        ///
        ///   startIndex:
        ///     The starting position within value.
        ///
        /// Returns:
        ///     A 16-bit unsigned integer formed by two bytes beginning at startIndex.
        ///
        /// Exceptions:
        ///   System.ArgumentException:
        ///     startIndex equals the length of value minus 1.
        ///
        ///   System.ArgumentNullException:
        ///     value is null.
        ///
        ///   System.ArgumentOutOfRangeException:
        ///     startIndex is less than zero or greater than the length of value minus 1.
        ///</summary>
        public static ushort ToUInt16(byte[] value, int startIndex)
        {
            if (IsLittleEndian)
            {
                return BitConverter.ToUInt16(value, startIndex);
            }
            return BitConverter.ToUInt16(value.Reverse().ToArray(), value.Length - sizeof(UInt16) - startIndex);
        }

        ///
        /// <summary>
        ///     Returns a 32-bit unsigned integer converted from four bytes at a specified
        ///     position in a byte array.
        ///
        /// Parameters:
        ///   value:
        ///     An array of bytes.
        ///
        ///   startIndex:
        ///     The starting position within value.
        ///
        /// Returns:
        ///     A 32-bit unsigned integer formed by four bytes beginning at startIndex.
        ///
        /// Exceptions:
        ///   System.ArgumentException:
        ///     startIndex is greater than or equal to the length of value minus 3, and is
        ///     less than or equal to the length of value minus 1.
        ///
        ///   System.ArgumentNullException:
        ///     value is null.
        ///
        ///   System.ArgumentOutOfRangeException:
        ///     startIndex is less than zero or greater than the length of value minus 1.
        ///</summary>
        public static uint ToUInt32(byte[] value, int startIndex)
        {
            if (IsLittleEndian)
            {
                return BitConverter.ToUInt32(value, startIndex);
            }
            return BitConverter.ToUInt32(value.Reverse().ToArray(), value.Length - sizeof(UInt32) - startIndex);
        }

        ///
        /// <summary>
        ///     Returns a 64-bit unsigned integer converted from eight bytes at a specified
        ///     position in a byte array.
        ///
        /// Parameters:
        ///   value:
        ///     An array of bytes.
        ///
        ///   startIndex:
        ///     The starting position within value.
        ///
        /// Returns:
        ///     A 64-bit unsigned integer formed by the eight bytes beginning at startIndex.
        ///
        /// Exceptions:
        ///   System.ArgumentException:
        ///     startIndex is greater than or equal to the length of value minus 7, and is
        ///     less than or equal to the length of value minus 1.
        ///
        ///   System.ArgumentNullException:
        ///     value is null.
        ///
        ///   System.ArgumentOutOfRangeException:
        ///     startIndex is less than zero or greater than the length of value minus 1.
        ///</summary>
        public static ulong ToUInt64(byte[] value, int startIndex)
        {
            if (IsLittleEndian)
            {
                return BitConverter.ToUInt64(value, startIndex);
            }
            return BitConverter.ToUInt64(value.Reverse().ToArray(), value.Length - sizeof(UInt64) - startIndex);
        }
    }
    public static class ChannelUtils
    {
        public static byte[] EncodeChannel(string channelHex, long amount)
        {
            byte[] HASH_CHANNEL_SIGN = { 0x43, 0x4C, 0x4D, 0x00 };
            byte[] AMOUNT_HEX_ARRAY = Bits.GetBytes(amount);
            byte[] CHANNEL_HEX_ARRAY = Convert.FromHexString(channelHex);

            byte[] rv = new byte[HASH_CHANNEL_SIGN.Length + CHANNEL_HEX_ARRAY.Length + AMOUNT_HEX_ARRAY.Length];
            System.Buffer.BlockCopy(HASH_CHANNEL_SIGN, 0, rv, 0, HASH_CHANNEL_SIGN.Length);
            System.Buffer.BlockCopy(CHANNEL_HEX_ARRAY, 0, rv, HASH_CHANNEL_SIGN.Length, CHANNEL_HEX_ARRAY.Length);
            System.Buffer.BlockCopy(AMOUNT_HEX_ARRAY, 0, rv, HASH_CHANNEL_SIGN.Length + CHANNEL_HEX_ARRAY.Length, AMOUNT_HEX_ARRAY.Length);
            return rv;
        }
    }
    public static class NFTokenID
    {
        public static string ParseNFTokenID(string nftokenID)
        {
            var expectedLength = 64;
            if (nftokenID.Length != expectedLength)
            {
                throw new RippleException($"Attempting to parse a nftokenID with length {nftokenID.Length}, but expected a token with length {expectedLength}");
            }

            var scrambledTaxon = new BigInteger(nftokenID.Substring(48, 8), 16).LongValue;
            var sequence = new BigInteger(nftokenID.Substring(56, 8), 16).LongValue;

            var NFTokenIDData = new
            {
                NFTokenID = nftokenID,
                Flags = new BigInteger(nftokenID.Substring(0, 4), 16).LongValue,
                TransferFee = new BigInteger(nftokenID.Substring(4, 4), 16).LongValue,
                //Issuer = EncodeAccountID(Encoding.UTF8.GetBytes(nftokenID.Substring(8, 40))),
                Taxon = UnscrambleTaxon(scrambledTaxon, sequence),
                Sequence = sequence
            };

            var issuer = new BigInteger(nftokenID.Substring(8, 40), 16);
            var t1 = issuer.ToBinaryString();
            var t11 = issuer.ToHexadecimalString();
            var t111 = issuer.ToOctalString();


            return JsonConvert.SerializeObject(NFTokenIDData);
        }

        private static long UnscrambleTaxon(long taxon, long tokenSeq)
        {
            return (taxon ^ (384160001 * tokenSeq + 2459)) % 4294967296;
        }

        private static string EncodeAccountID(byte[] bytes)
        {
            var accountID = new StringBuilder();
            var versionByte = bytes[0];
            var versionByteHex = versionByte.ToString("X2");
            accountID.Append(versionByteHex);
            var accountIDBytes = bytes.Skip(1).ToArray();
            var accountIDHex = BitConverter.ToString(accountIDBytes).Replace("-", "");
            accountID.Append(accountIDHex);
            return accountID.ToString();
        }
    }    /// <summary>
         /// Extension methods to convert <see cref="System.Numerics.BigInteger"/>
         /// instances to hexadecimal, octal, and binary strings.
         /// </summary>
    public static class BigIntegerExtensions
    {
        /// <summary>
        /// Converts a <see cref="BigInteger"/> to a binary string.
        /// </summary>
        /// <param name="bigint">A <see cref="BigInteger"/>.</param>
        /// <returns>
        /// A <see cref="System.String"/> containing a binary
        /// representation of the supplied <see cref="BigInteger"/>.
        /// </returns>
        public static string ToBinaryString(this BigInteger bigint)
        {
            var bytes = bigint.ToByteArray();
            var idx = bytes.Length - 1;

            // Create a StringBuilder having appropriate capacity.
            var base2 = new StringBuilder(bytes.Length * 8);

            // Convert first byte to binary.
            var binary = Convert.ToString(bytes[idx], 2);

            // Ensure leading zero exists if value is positive.
            if (binary[0] != '0' && bigint.SignValue == 1)
            {
                base2.Append('0');
            }

            // Append binary string to StringBuilder.
            base2.Append(binary);

            // Convert remaining bytes adding leading zeros.
            for (idx--; idx >= 0; idx--)
            {
                base2.Append(Convert.ToString(bytes[idx], 2).PadLeft(8, '0'));
            }

            return base2.ToString();
        }

        /// <summary>
        /// Converts a <see cref="BigInteger"/> to a hexadecimal string.
        /// </summary>
        /// <param name="bigint">A <see cref="BigInteger"/>.</param>
        /// <returns>
        /// A <see cref="System.String"/> containing a hexadecimal
        /// representation of the supplied <see cref="BigInteger"/>.
        /// </returns>
        public static string ToHexadecimalString(this BigInteger bigint)
        {
            return bigint.ToString(16);
        }

        /// <summary>
        /// Converts a <see cref="BigInteger"/> to a octal string.
        /// </summary>
        /// <param name="bigint">A <see cref="BigInteger"/>.</param>
        /// <returns>
        /// A <see cref="System.String"/> containing an octal
        /// representation of the supplied <see cref="BigInteger"/>.
        /// </returns>
        public static string ToOctalString(this BigInteger bigint)
        {
            var bytes = bigint.ToByteArray();
            var idx = bytes.Length - 1;

            // Create a StringBuilder having appropriate capacity.
            var base8 = new StringBuilder(((bytes.Length / 3) + 1) * 8);

            // Calculate how many bytes are extra when byte array is split
            // into three-byte (24-bit) chunks.
            var extra = bytes.Length % 3;

            // If no bytes are extra, use three bytes for first chunk.
            if (extra == 0)
            {
                extra = 3;
            }

            // Convert first chunk (24-bits) to integer value.
            int int24 = 0;
            for (; extra != 0; extra--)
            {
                int24 <<= 8;
                int24 += bytes[idx--];
            }

            // Convert 24-bit integer to octal without adding leading zeros.
            var octal = Convert.ToString(int24, 8);

            // Ensure leading zero exists if value is positive.
            if (octal[0] != '0' && bigint.SignValue == 1)
            {
                base8.Append('0');
            }

            // Append first converted chunk to StringBuilder.
            base8.Append(octal);

            // Convert remaining 24-bit chunks, adding leading zeros.
            for (; idx >= 0; idx -= 3)
            {
                int24 = (bytes[idx] << 16) + (bytes[idx - 1] << 8) + bytes[idx - 2];
                base8.Append(Convert.ToString(int24, 8).PadLeft(8, '0'));
            }

            return base8.ToString();
        }
    }

}

