﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Opten.Web.Mvc.Integration.Test.Startup))]
namespace Opten.Web.Mvc.Integration.Test
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
