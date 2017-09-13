using _Core.Helpers;
using _Core.Ops;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Web.Http.Cors;
using Telerik.Sitefinity.Abstractions;

namespace SitefinityWebApp.Api.ImageSubmission
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ImageSubmissionController : ApiController
    {
         [HttpPost]
        public HttpResponseMessage Post([FromBody]ImageSubmissionModel model)
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            responseMessage.StatusCode = HttpStatusCode.OK;

            #region Field Validation

            ErrorsManager errorsManager = new ErrorsManager();
            if (string.IsNullOrEmpty(model.FullName)) errorsManager.AddError(new ValidationException("Name is empty.", "FullName"));
            if (string.IsNullOrEmpty(model.Company)) errorsManager.AddError(new ValidationException("Company is empty.", "Company"));
            if (string.IsNullOrEmpty(model.Email)) errorsManager.AddError(new ValidationException("Email is empty.","Email"));
            else
            {
                if (!(Regex.IsMatch(model.Email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase)))
                    errorsManager.AddError(new ValidationException("Email is not valid.", "Email"));
            }
            if (errorsManager.HasErrors)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(errorsManager.ToValidationString())});
            #endregion
            try
            {
                ImageSubmissionOpsMgmt.InsertItem(model.FullName, model.Company, model.PhoneNumber, model.Email, model.Comments);

                EmailEngine emailEngine = new EmailEngine(string.Empty, "ImageSubmissionTemplate", "A new image upload request has been submitted.");
                emailEngine.AddAdminReceivers();
                emailEngine.AddMergingField("FullName", model.FullName);
                emailEngine.AddMergingField("Company", model.Company);
                emailEngine.AddMergingField("PhoneNumber", model.PhoneNumber);
                emailEngine.AddMergingField("Email", model.Email);
                emailEngine.AddMergingField("Content", model.Comments);


                emailEngine.SendEmail();

                return responseMessage;
            }
            catch (Exception ex)
            {
                Log.Write(ex, System.Diagnostics.TraceEventType.Error);
                errorsManager.AddError(new ValidationException("Unknown error has occured.","General"));
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(errorsManager.ToValidationString()) });
            }

        }
    }
}