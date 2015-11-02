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

using iHUDServer;
using iHUDServer.Interfaces;
using Microsoft.Owin;
using Owin;
using System;
using System.Threading;

namespace Owin
{
    /// <summary>
    /// Extensions method for OWin
    /// </summary>    
    public static class OwinExtensions
    {
        /// <summary>
        /// Registers the on application disposing method.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="cleanUpMethod">The clean up method.</param>
        public static void RegisterOnAppDisposing(this IAppBuilder app, Action cleanUpMethod)
        {
            var context = new OwinContext(app.Properties);
            var token = context.Get<CancellationToken>("host.OnAppDisposing");
            if (token != CancellationToken.None)
            {
                token.Register(cleanUpMethod);
            }
        }  
    }
}
