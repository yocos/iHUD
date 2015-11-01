using iHUDServer.Interfaces;
using iHUDServer.IRacing;
using iRacingSdkWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace iHUDServer.IRacing
{
    public class RaceDataReceiver : ILiveDataReceiver
    {
        private readonly SdkWrapper _cnx;
        private readonly CancellationTokenSource source = new CancellationTokenSource();
        private readonly IRaceServerProxy _serverProxy;

        public RaceDataReceiver(SdkWrapper cnx, IRaceServerProxy server)
        {
            Guard.Argument.IsNotNull(cnx, "cnx");
            Guard.Argument.IsNotNull(server, "server");

            _cnx = cnx;                            
            _serverProxy = server;
        }             

        public void StartListenning()
        {
            _cnx.Connected += On_Connected;
            _cnx.Disconnected += On_Disconnected;
            _cnx.TelemetryUpdated += On_TelemetryUpdated;
            _cnx.SessionInfoUpdated += _cnx_SessionInfoUpdated;

            _cnx.Start();
        }

        private void _cnx_SessionInfoUpdated(object sender, SdkWrapper.SessionInfoUpdatedEventArgs e)
        {
            _serverProxy.Broadcast(e.SessionInfo);
        }

        private void On_TelemetryUpdated(object sender, SdkWrapper.TelemetryUpdatedEventArgs e)
        {
            _serverProxy.Broadcast(e.TelemetryInfo);
        }


        private void On_Disconnected(object sender, EventArgs e)
        {
            _serverProxy.Broadcast("Disconnected");
        }

        private void On_Connected(object sender, EventArgs e)
        {
            _serverProxy.Broadcast("Connected");
        }

        public void StopListenning()
        {

            source.Cancel();
            _cnx.Stop();
        }

    }
}
