﻿using System;
using System.Linq;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace SitefinityWebApp.Application.Fields.PageSelector
{
    public interface IPageSelectorFieldDefinition : IFieldControlDefinition
    {
        /// <summary>
        /// Gets or sets the dynamic module type.
        /// </summary>
        /// <value>The dynamic module type.</value>
        string DynamicModuleType { get; set; }
    }
}