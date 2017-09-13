using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Core.ImageCompression
{
    public class ImageCompressionEngine
    {
        public static string GetApiUrl(ImageCompressionEngineType engineType)
        {
            switch (engineType)
            {
                case ImageCompressionEngineType.KRAKEN:
                default:
                    return string.Empty;

                case ImageCompressionEngineType.TINYPNG:
                    return "";

                case ImageCompressionEngineType.OPTIMUS:
                    return "https://api.optimus.io/";
            }
        }

        public static string GetApiTag(ImageCompressionEngineType engineType)
        {
            switch (engineType)
            {
                case ImageCompressionEngineType.KRAKEN:
                    return "KRAKEN";

                case ImageCompressionEngineType.TINYPNG:
                    return "TINYPNG";

                case ImageCompressionEngineType.OPTIMUS:
                    return "OPTIMUS";

                default:
                    return string.Empty;
            }
        }

        public static IImageCompressionApi GetCompressionApi(ImageCompressionEngineType engineType, string apiKey, string secretKey)
        {
            switch (engineType)
            {
               case ImageCompressionEngineType.TINYPNG:
                    return new TinyPNGApi(apiKey, GetApiUrl(engineType));

                case ImageCompressionEngineType.OPTIMUS:
                    return new OptimusApi(apiKey, GetApiUrl(engineType));

                default:
                    return null;
            }
        }
    }
}
