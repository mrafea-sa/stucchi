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

namespace SitefinityWebApp.Api.ContactUs
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ContactController : ApiController
    {
         [HttpPost]
        public HttpResponseMessage Post([FromBody]ContactModel model)
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            responseMessage.StatusCode = HttpStatusCode.OK;

            #region Field Validation

            ErrorsManager errorsManager = new ErrorsManager();
            if (string.IsNullOrEmpty(model.FirstName)) errorsManager.AddError(new ValidationException("First name is empty.","FirstName"));
            if (string.IsNullOrEmpty(model.LastName)) errorsManager.AddError(new ValidationException("Last name is empty.", "LastName"));
            if (string.IsNullOrEmpty(model.Company)) errorsManager.AddError(new ValidationException("Company is empty.", "Company"));
           
            if (errorsManager.HasErrors)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(errorsManager.ToValidationString())});
            #endregion
            try
            {
                ContactUsOpsMgmt.InsertItem(model.FirstName, model.LastName, model.Address, model.Address2, model.Company,
                    model.City, model.State, model.Zip, model.Company, model.Phone,  model.Message, model.ReasonsForContact, model.HasOptedForSubscription);

                EmailEngine emailEngine = new EmailEngine(string.Empty, "ContactTemplate", "A new contact request has been submitted.");
                emailEngine.AddAdminReceivers();
                emailEngine.AddMergingField("FirstName", model.FirstName);
                emailEngine.AddMergingField("LastName", model.LastName);
                emailEngine.AddMergingField("Address", model.Address);
                emailEngine.AddMergingField("Address2", model.Address);
                emailEngine.AddMergingField("Company", model.Company);
                emailEngine.AddMergingField("City", model.City);
                emailEngine.AddMergingField("State", model.State);
                emailEngine.AddMergingField("Zip", model.Zip);
                emailEngine.AddMergingField("Phone", model.Phone);
                emailEngine.AddMergingField("Country", model.Country);
                emailEngine.AddMergingField("Message", model.Message);
                emailEngine.AddMergingField("ReasonsForContact", model.ReasonsForContact);
                emailEngine.AddMergingField("HasOptedForSubscription", model.HasOptedForSubscription);


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