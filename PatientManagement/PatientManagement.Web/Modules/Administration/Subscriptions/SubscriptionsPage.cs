﻿

[assembly:Serenity.Navigation.NavigationLink(int.MaxValue, "Administration/Subscriptions", typeof(PatientManagement.Administration.Pages.SubscriptionsController))]

namespace PatientManagement.Administration.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [PageAuthorize(typeof(Entities.SubscriptionsRow))]
    public class SubscriptionsController : Controller
    {
        [Route("Administration/Subscriptions")]
        public ActionResult Index()
        {
            return View("~/Modules/Administration/Subscriptions/SubscriptionsIndex.cshtml");
        }
    }
}