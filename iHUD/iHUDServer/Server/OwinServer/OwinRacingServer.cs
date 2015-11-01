using iHUDServer.Interfaces;
using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iRacingSdkWrapper;
using iHUDServer.hubs;
using Microsoft.AspNet.SignalR;

namespace iHUDServer
{
    public class OwinRacingServer : IRaceServer, IDisposable
    {
        private IHubContext myHub;
      

        public void Start()
        {
            _owinServer = WebApp.Start<Startup>("http://*:6778/");
            myHub = GlobalHost.ConnectionManager.GetHubContext<WelcomeHub>();
        }

        public void Stop()
        {
            if (_owinServer != null)
            {
                _owinServer.Dispose();
                _owinServer = null;
            }
        }

        public void Dispose()
        {
            if (_owinServer != null)
            {
                _owinServer.Dispose();
            };
        }

        public void Broadcast(TelemetryInfo telemetryInfo)
        {
            myHub.Clients.All.ReceiveMessage(Math.Round((Convert.ToSingle(telemetryInfo.Speed.Value) * 3600 / 1000), 2, MidpointRounding.AwayFromZero));
        }

        public void Broadcast(SessionInfo sessionInfo)
        {
            myHub.Clients.All.ReceiveMessage("sessioninfo received !");
        }

        public void Broadcast(string message)
        {
            myHub.Clients.All.ReceiveMessage(string.Format("Message : {0}", message));
        }

        private IDisposable _owinServer;
    }
}
