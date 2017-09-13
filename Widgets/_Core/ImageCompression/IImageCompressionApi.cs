using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Core.ImageCompression
{
    public interface IImageCompressionApi
    {
        byte[] CompressImage(byte[] inputImage);
    }
}
