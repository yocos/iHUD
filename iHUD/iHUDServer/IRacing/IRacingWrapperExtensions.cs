﻿/*******************************************************************
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
using iHUDServer.IRacing;
using iRacingSdkWrapper;
using Owin;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHUDServer.IRacing
{
    public static class IRacingWrapperExtensions
    {
        public static void UseIRacingWrapper(this ApplicationContext app)
        {
            app.ServiceProvider.RegisterSingleton<SdkWrapper>();
            app.ServiceProvider.RegisterSingleton<ILiveDataReceiver, RaceDataReceiver>();
        }

    }
}
