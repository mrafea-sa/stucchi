using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace _Core.ImageCompression
{
    public class OptimusApi : IImageCompressionApi
    {
        private string _apiKey;
        private string _shrinkApiEndpoint;

        public OptimusApi(string apiKey, string shrinkApiEndpoint)
        {
            this._apiKey = apiKey;
            this._shrinkApiEndpoint = shrinkApiEndpoint;
        }

        public byte[] CompressImage(byte[] inputImage)
        {
            byte[] response = null;
            WebClient client = new WebClient();
            //string auth = Convert.ToBase64String(Encoding.UTF8.GetBytes("api:" + this.ApiKey));
            //client.Headers.Add(HttpRequestHeader.Authorization, "Basic " + auth);

            string composedAddress = string.Concat(this._shrinkApiEndpoint, this._apiKey, "?optimize");
            throw new Exception(composedAddress);
            try
            {
                client.UploadData(composedAddress, inputImage);

                if (!string.IsNullOrEmpty(client.ResponseHeaders["Location"]))
                    response = client.DownloadData(client.ResponseHeaders["Location"]);
            }
            catch (Exception ex)
            {
                throw ex;
                return null;
            }
            return response;
        }
    }
}