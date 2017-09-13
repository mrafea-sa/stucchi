using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace _Core.ImageCompression
{
    public class TinyPNGApi : IImageCompressionApi
    {
        private string _apiKey;
        private string _shrinkApiEndpoint;

        public TinyPNGApi(string apiKey, string shrinkApiEndpoint)
        {
            this._apiKey = apiKey;
            this._shrinkApiEndpoint = shrinkApiEndpoint;
        }

        public byte[] CompressImage(byte[] inputImage)
        {
            byte[] response = null;
            WebClient client = new WebClient();
            string auth = Convert.ToBase64String(Encoding.UTF8.GetBytes("api:" + this._apiKey));
            client.Headers.Add(HttpRequestHeader.Authorization, "Basic " + auth);

            try
            {
                client.UploadData(this._shrinkApiEndpoint, inputImage);

                if (!string.IsNullOrEmpty(client.ResponseHeaders["Location"]))
                    response = client.DownloadData(client.ResponseHeaders["Location"]);
            }
            catch (Exception ex)
            {
                return null;
            }
            return response;
        }
    }
}