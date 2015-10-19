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

using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRHub.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            IHubProxy _hub;
            string url = @"http://192.168.1.93:6778/";
            var connection = new HubConnection(url);
            _hub = connection.CreateHubProxy("WelcomeHub");
            _hub.On("ReceiveMessage", x => Console.WriteLine(x));

            Console.WriteLine("Client starded: Waiting datas....");

            connection.Start().Wait();

            string line = null;
            bool stop = false;
            while ((line = System.Console.ReadLine()) != null && !stop)
            {
                if (line.Equals("exit"))
                    stop = true;
            }
        }
    }
}
