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

using iRacingSdkWrapper;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;

namespace iHUDServer.hubs
{
    [HubName("TelemetryHub ")]
    public class TelemetryHub : Hub
    {
        public void SendTelemetry(TelemetryInfo telemetry)
        {
            Clients.All.SendTelemetry(telemetry);
        }       
    }
}
