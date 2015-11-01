using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iHUDServer
{
    public class ApplicationContext : IDisposable
    {

        public Container ServiceProvider { get { return _Container; } }

        private static readonly Container _Container = new Container();

        public void Dispose()
        {
            _Container.Dispose();
            GC.SuppressFinalize(this);
        }


    }
}
