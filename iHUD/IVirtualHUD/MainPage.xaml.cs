using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace IVirtualHUD
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        TaskScheduler _ctx;

        public MainPage()
        {
            this.InitializeComponent();

            IHubProxy _hub;
            string url = @"http://192.168.1.93:6778/";
            //string url = @"http://localhost:6778/";
            var connection = new HubConnection(url);
            _hub = connection.CreateHubProxy("WelcomeHub");
            _hub.On<string>("ReceiveMessage", x => SetSpeed(x));

           _ctx = TaskScheduler.FromCurrentSynchronizationContext();

            connection.Start().Wait();

        }

        private void SetSpeed(string msg)
        {
            // It's possible to start a task directly on
            // the UI thread, but not common...
            var token = Task.Factory.CancellationToken;
            Task.Factory.StartNew(() =>
            {
                this.speed.Text = msg;
            }, token, TaskCreationOptions.None, _ctx);
            
        }
    }
}
