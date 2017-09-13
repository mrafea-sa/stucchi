using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitefinityWebApp.Api.ImageSubmission
{
    public class ImageSubmissionModel
    {
        public string FullName { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Comments { get; set; }
    }
}