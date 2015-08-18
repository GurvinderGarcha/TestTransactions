using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;

namespace WebApplication2.Infrastructure
{
    public static class MvcUnityContainer
    {
        public static IUnityContainer Container { get; set; }
    }
}