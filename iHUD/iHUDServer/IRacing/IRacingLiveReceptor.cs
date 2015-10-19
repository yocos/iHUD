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

using iHUDServer.hubs;
using iRacingSdkWrapper;
using Microsoft.AspNet.SignalR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace iHUDServer.IRacing
{
    public class IRacingLiveReceptor : IDataReceptor
    {
        SdkWrapper _cnx;
        CancellationTokenSource source = new CancellationTokenSource();
        //iRacingEvents events;
        public IRacingLiveReceptor(SdkWrapper iRacingCnx)
        {
            if (iRacingCnx == null) throw new ArgumentNullException("iRacindCnx");

            _cnx = iRacingCnx;           
        }       

        public void StartListenning()
        {
            _cnx.Connected += On_Connected;
            _cnx.Disconnected += On_Disconnected;
            _cnx.TelemetryUpdated += On_TelemetryUpdated;
            _cnx.SessionInfoUpdated += _cnx_SessionInfoUpdated;

            _cnx.Start();

            Task.Run(() =>
            {
                while (true)
                {
                    var context = GlobalHost.ConnectionManager.GetHubContext<WelcomeHub>();
                    context.Clients.All.ReceiveMessage("iHUDServer is alive !");

                    Thread.Sleep(60000);
                }
            }, source.Token);
        }

        private void _cnx_SessionInfoUpdated(object sender, SdkWrapper.SessionInfoUpdatedEventArgs e)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<WelcomeHub>();            
            Console.WriteLine(string.Format("New session data !"));
        }

        private void On_TelemetryUpdated(object sender, SdkWrapper.TelemetryUpdatedEventArgs e)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<WelcomeHub>();
            context.Clients.All.ReceiveMessage(string.Format("Current Speed is {0}", Math.Round((Convert.ToSingle(e.TelemetryInfo.Speed.Value) * 3600 / 1000), 2)));
            Console.WriteLine(string.Format("Current Speed is {0} {1}", e.TelemetryInfo.Speed.Value, e.TelemetryInfo.Speed.Unit));
            Console.WriteLine(string.Format("Current Speed is {0}", Math.Round((Convert.ToSingle(e.TelemetryInfo.Speed.Value) * 3600 / 1000), 2)));
        }
     

        private void On_Disconnected(object sender, EventArgs e)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<WelcomeHub>();

            context.Clients.All.ReceiveMessage("Disconnected from iRacing Server");
            Console.WriteLine("Disconnected from iRacing Server");
        }

        private void On_Connected(object sender, EventArgs e)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<WelcomeHub>();

            context.Clients.All.ReceiveMessage("Connected to iRacing Server");
            Console.WriteLine("Connected to iRacing Server");
        }

        public void StopListenning()
        {        

            source.Cancel();
            _cnx.Stop();
        }
    }
}

