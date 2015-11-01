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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iHUDServer.Interfaces
{
    /// <summary>
    /// Describe a Broadcast server
    /// </summary>
    public interface IRaceServer
    {
        /// <summary>
        /// Starts server.
        /// </summary>
        void Start();

        /// <summary>
        /// Stops server.
        /// </summary>
        void Stop();

        /// <summary>
        /// Broadcasts the specified racing information.
        /// </summary>
        /// <param name="telemetryInfo">The telemetry information.</param>
        void Broadcast(TelemetryInfo telemetryInfo);

        /// <summary>
        /// Broadcasts the specified racing information.
        /// </summary>
        /// <param name="sessionInfo">The session information.</param>
        void Broadcast(SessionInfo sessionInfo);


        /// <summary>
        /// Broadcasts the specified racing information.
        /// </summary>
        /// <param name="message">A racing message.</param>
        void Broadcast(string message);

    }
}
