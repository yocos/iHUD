using Owin;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iHUDServer.DependencyResolver
{
    internal static class SimpleInjectorExtensions
    {
        internal static Container UseSimpleInjector(this IAppBuilder app)
        {
            return new Container();
        }
    }
}
