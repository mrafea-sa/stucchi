using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _Core.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SitefinityWebApp.Api
{
    public class ErrorsManager
    {
        private IList<Exception> _errors;

        public ErrorsManager()
        {
            _errors = new List<Exception>();
        }

        public void AddError(Exception error)
        {

            _errors.Add(error);
        }

        public override string ToString()
        {
            string content = string.Empty;
            foreach (Exception error in _errors)
                content = content.Concatenate(error.Message, "<br/>");

            return content;
        }

        public bool HasErrors
        {
            get
            {
                return _errors.Count != 0;
            }
        }

        public string ToValidationString()
        {
            var settings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                ContractResolver = new DefaultContractResolver()
                {
                    IgnoreSerializableInterface = true
                }
            };

            return JsonConvert.SerializeObject(_errors, settings);
        }
    }
}