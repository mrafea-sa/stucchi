using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace _Core.Extensions
{
    public static class StringExtension
    {
        // This is the extension method.
        // The first parameter takes the "this" modifier
        // and specifies the type for which the method is defined.
        public static string TrimAndReduce(this string str)
        {
            return ConvertWhitespacesToSingleSpaces(str).Trim();
        }

        public static string ConvertWhitespacesToSingleSpaces(this string value)
        {
            return Regex.Replace(value, @"\s+", " ");
        }

        public static string ConvertToImageSeo(this string value)
        {
            if (value.LastIndexOf('?') > 0) return value.Sub(0, value.LastIndexOf('?') - 1);
            return value;
            //return (new Regex("\\?sfvrsn=([0-9]+)", RegexOptions.Singleline)).Replace(value, "");
        }

        public static string RetrieveImageFromThumb(this string value)
        {
            var imageUri = new Uri(value);
            string host = imageUri.Host;
            value = value.Replace("http://", "").Replace("https://", "").Replace(imageUri.Host, "");

            if (value.IndexOf(".tmb") > 0)
            {
                var remainingString = value.Substring(value.IndexOf(".tmb") + 1);
                var strinToReplace = remainingString.Substring(0, remainingString.IndexOf(".") + 1);
                value = value.Replace(strinToReplace, "");
            }
            value = value.Replace("media/", "");
            return string.Concat(ConfigurationManager.AppSettings["AssetsUrl"], value);

        }

        public static Guid ToGuid(this string value)
        {
            Guid guidValue = Guid.Empty;
            if (!Guid.TryParse(value, out guidValue)) guidValue = Guid.Empty;

            return guidValue;
        }

        public static string Base64Encode(this string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static IList<Guid> ToGuidList(this string content, char splitter = ',')
        {
            IList<Guid> results = new List<Guid>();

            string[] items = content.Split(splitter);
            foreach (var item in items)
            {
                try
                {
                    results.Add(Guid.Parse(item));
                }
                catch (Exception)
                {

                }
            }

            return results;
        }

        public static string Base64Decode(this string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static string Concatenate(this string source, string value, string separator)
        {
            return source + (string.IsNullOrEmpty(source) ? "" : separator) + value;
        }


        /// <summary>
        /// Compresses the string.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static string CompressString(this string text)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(text);
            var memoryStream = new MemoryStream();
            using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
            {
                gZipStream.Write(buffer, 0, buffer.Length);
            }

            memoryStream.Position = 0;

            var compressedData = new byte[memoryStream.Length];
            memoryStream.Read(compressedData, 0, compressedData.Length);

            var gZipBuffer = new byte[compressedData.Length + 4];
            Buffer.BlockCopy(compressedData, 0, gZipBuffer, 4, compressedData.Length);
            Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gZipBuffer, 0, 4);
            return Convert.ToBase64String(gZipBuffer);
        }

        /// <summary>
        /// Decompresses the string.
        /// </summary>
        /// <param name="compressedText">The compressed text.</param>
        /// <returns></returns>
        public static string DecompressString(this string compressedText)
        {
            compressedText = compressedText.Base64Encode();
            byte[] gZipBuffer = Convert.FromBase64String(compressedText);
            using (var memoryStream = new MemoryStream())
            {
                int dataLength = BitConverter.ToInt32(gZipBuffer, 0);
                memoryStream.Write(gZipBuffer, 4, gZipBuffer.Length - 4);

                var buffer = new byte[dataLength];

                memoryStream.Position = 0;
                using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                {
                    gZipStream.Read(buffer, 0, buffer.Length);
                }

                return Encoding.UTF8.GetString(buffer);
            }
        }

    }
}
