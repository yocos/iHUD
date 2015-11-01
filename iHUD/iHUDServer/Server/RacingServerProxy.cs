using iHUDServer.Interfaces;
using iHUDServer.IRacing;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using iRacingSdkWrapper;

namespace iHUDServer
{
    public class RacingServerProxy : IRaceServerProxy, IDisposable
    {
        private IEnumerable<IRaceServer> _MyServers;

        public RacingServerProxy(IEnumerable<IRaceServer> servers)
        {
            Guard.Argument.IsNotNull(servers, "servers");
            _MyServers = servers;
        }

        public void Dispose()
        {
            //Stop Broadcast Servers 
            foreach (var srv in _MyServers)
            {
                srv.Stop();
                if (srv is IDisposable) (srv as IDisposable).Dispose();
            }

            GC.SuppressFinalize(this);
        }

        public void Start()
        {


            foreach (var item in _MyServers)
            {
                item.Start();
            }

        }

        public void Stop()
        {
            foreach (var item in _MyServers)
            {
                item.Stop();
            }

        }

        public void Broadcast(TelemetryInfo telemetryInfo)
        {
            foreach (var item in _MyServers)
            {
                item.Broadcast(telemetryInfo);
            }
        }

        public void Broadcast(SessionInfo sessionInfo)
        {
            foreach (var item in _MyServers)
            {
                item.Broadcast(sessionInfo);
            }
        }

        public void Broadcast(string message)
        {
            foreach (var item in _MyServers)
            {
                item.Broadcast(message);
            }
        }
    }
}
