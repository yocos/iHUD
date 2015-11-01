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

using iHUDServer.Interfaces;
using IHUDServer.IRacing;
using Microsoft.Owin.Hosting;
using Owin;
using SimpleInjector;
using System;
using System.Collections.Generic;

namespace iHUDServer
{
    class Program
    {
        static void Main(string[] args)
        {

            using (ApplicationContext myApp = new ApplicationContext())
            {
                //Create DI Container
                myApp.UseIRacingWrapper();
                // myApp.UseOwinRacingServer();
                myApp.ServiceProvider.Register<IRaceServerProxy, RacingServerProxy>(Lifestyle.Singleton);
                myApp.ServiceProvider.RegisterCollection<IRaceServer>(new OwinRacingServer());

                var proxy = myApp.ServiceProvider.GetInstance<IRaceServerProxy>();
                var DataReceiver = myApp.ServiceProvider.GetInstance<ILiveDataReceiver>();
                
                try
                {
                    proxy.Start();
                    DataReceiver.StartListenning();

                    Console.WriteLine("Server is started - Press Return to Exit.");
                    Console.ReadLine();
                }
                finally
                {
                    DataReceiver.StopListenning();
                    proxy.Stop();
                }

            }
        }
    }
}
