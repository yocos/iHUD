/*******************************************************************
 This file is part of iHUD.

 Copyright 2015 Yoann COSNIAM
 https://github.com/yocos/iHUD

 iRacingSDK is free software: you can redistribute it and/or modify
 it under the terms of the Apache V2.0 License.

 iHUD is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
 Apache V2.0 License for more details.

 You should have received a copy of the Apache License
 along with iHUD.  If not, see <http://www.apache.org/licenses/>.     
*******************************************************************/
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Owin;
using iHUDServer.IRacing;
using SimpleInjector;
using iHUDServer.DependencyResolver;
using Microsoft.Owin;
using System.Threading;

namespace iHUDServer
{
    public class Startup
    {
        
        public void Configuration(IAppBuilder app)
        {
            var _container = app.UseSimpleInjector();

            app.Map("/signalr", map =>
            {
                // Setup the cors middleware to run before SignalR.
                // By default this will allow all origins. You can 
                // configure the set of origins and/or http verbs by
                // providing a cors options with a different policy.
                map.UseCors(CorsOptions.AllowAll);

                var hubConfiguration = new HubConfiguration
                {
                    // You can enable JSONP by uncommenting line below.
                    // JSONP requests are insecure but some older browsers (and some
                    // versions of IE) require JSONP to work cross domain
                    // EnableJSONP = true
                };

                // Run the SignalR pipeline. We're not using MapSignalR
                // since this branch is already runs under the "/signalr"
                // path.
                map.RunSignalR(hubConfiguration);
            });
           
            app.UseIRacing(_container);

            _container.RegisterSingleton<IDataReceptor, IRacingLiveReceptor>();

            // 3. Optionally verify the container's configuration.
            _container.Verify();


            var iRacingReceptor = _container.GetInstance<IDataReceptor>();            

            var context = new OwinContext(app.Properties);
            var token = context.Get<CancellationToken>("host.OnAppDisposing");
            if (token != CancellationToken.None)
            {
                token.Register(() =>
                {
                    iRacingReceptor.StopListenning();
                });
            }

            //Start process
            iRacingReceptor.StartListenning();

        }
    }
}