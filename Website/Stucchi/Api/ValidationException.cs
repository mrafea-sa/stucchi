using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SitefinityWebApp.Api
{
    [DataContract]
    public class ValidationException : Exception
    {
        [DataMember]
        public string ControlId { get; set; }

        [DataMember]
        public string ErrorMessage { get; set; }

        public ValidationException(string message, string controlId):base(message)
        {
            this.ControlId = controlId;
            this.ErrorMessage = message;
        }

    }
}