﻿using System.Web.Mvc;
using AmplaData.AmplaSecurity2007;
using AmplaData.Web.Authentication;
using AmplaData.Web.Authentication.Forms;
using AmplaData.Web.Models;
using AmplaData.Web.Sessions;
using AmplaData.Web.Wrappers;
using NUnit.Framework;

namespace AmplaData.Web.Controllers
{
    [TestFixture]
    public class AccountControllersUnitTests : TestFixture
    {
        private SimpleHttpContext context;
        private SimpleSecurityWebServiceClient webServiceClient;
        private IAmplaUserStore amplaUserStore;
        private IAmplaUserService amplaUserService;
        private AccountController controller;

        protected override void OnSetUp()
        {
            base.OnSetUp();
            webServiceClient = new SimpleSecurityWebServiceClient("User");
            amplaUserStore = new AmplaUserStore();
            amplaUserService = new AmplaUserService(webServiceClient, amplaUserStore);

            context = SimpleHttpContext.Create("http://localhost");

            controller = new AccountController(amplaUserService, FormsAuthenticationService, AmplaSessionStorage);
        }

        private IAmplaSessionStorage AmplaSessionStorage
        {
            get
            {
                return new AmplaSessionStorage(context.Session);
            }
        }

        private IFormsAuthenticationService FormsAuthenticationService
        {
            get
            {
                return new FormsAuthenticationService(context.Request, context.Response);
            }
        }

        [Test]
        public void Login()
        {
            const string returnUrl = "/Production";

            SimpleLoginModel model = new SimpleLoginModel {UserName = "User", Password = "password"};
            ActionResult result = controller.Login(model, returnUrl);

            Assert.That(result, Is.TypeOf<RedirectResult>());
            RedirectResult redirectResult = (RedirectResult)result;
            Assert.That(redirectResult.Url, Is.EqualTo(returnUrl));
            Assert.That(redirectResult.Permanent, Is.False);

            Assert.That(AmplaSessionStorage.GetAmplaSession(), Is.Not.Empty);
        }

        [Test]
        public void LoginWithInvalidUserName()
        {
            const string returnUrl = "/Production";

            SimpleLoginModel model = new SimpleLoginModel { UserName = "Invalid", Password = "password" };
            ActionResult result = controller.Login(model, returnUrl);

            Assert.That(result, Is.TypeOf<ViewResult>());
            ViewResult viewResult = (ViewResult)result;
            Assert.That(viewResult.Model, Is.TypeOf<SimpleLoginModel>());

            Assert.That(AmplaSessionStorage.GetAmplaSession(), Is.Empty);
        }


    }
}