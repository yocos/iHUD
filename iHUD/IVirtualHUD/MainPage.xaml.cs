using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using tm1638.TM16XX;
using Windows.Devices.Gpio;
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

        GpioController _GpioController;
        ManualResetEvent _resetEvent;


        public MainPage()
        {
            this.InitializeComponent();

            IHubProxy _hub;
            string url = @"http://192.168.1.93:6778/";
            //string url = @"http://localhost:6778/";
            var connection = new HubConnection(url);

            _hub = connection.CreateHubProxy("TelemetryHub");
            _hub.On<string>("SendTelemetry", data => SetTelemetry(data));

            _ctx = TaskScheduler.FromCurrentSynchronizationContext();

            try
            {
                connection.Start().Wait();
            }
            catch (Exception)
            {

            }


        }

        private void SetTelemetry(string msg)
        {
            // It's possible to start a task directly on
            // the UI thread, but not common...
            var token = Task.Factory.CancellationToken;
            Task.Factory.StartNew(() =>
            {
                this.speed.Text = msg;
            }, token, TaskCreationOptions.None, _ctx);

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            _GpioController = GpioController.GetDefault();
            _resetEvent = new ManualResetEvent(false);

            if (this._GpioController != null)
            {

                using (GpioPin data = _GpioController.OpenPin(17))
                using (GpioPin clock = _GpioController.OpenPin(27))
                using (GpioPin strobe = _GpioController.OpenPin(22))
                {
                    data.SetDriveMode(GpioPinDriveMode.Output);
                    clock.SetDriveMode(GpioPinDriveMode.Output);
                    strobe.SetDriveMode(GpioPinDriveMode.Output);


                    var display = new TM1638(data, clock, strobe, true, 1);
                    display.setDisplayToString("COUCOU !", 0, 0);

                    for (byte i = 0; i < 8; i++)
                    {
                        Task.Delay(500 / 2).Wait();

                        display.setLED(TM1638.TM1638_LED_COLOR.RED, i);

                        Task.Delay(500 / 2).Wait();

                        display.setLED(TM1638.TM1638_LED_COLOR.GREEN, i);


                    }

                    Task.Delay(3000 / 2).Wait();

                    display.setDisplayToString("BISOUS! ", 0, 0);

                    Task.Delay(3000 / 2).Wait();

                    for (int i = 0; i < 360; i++)
                    {
                        display.setDisplayToString(String.Format("4    {0}", i.ToString().PadRight(3, ' ')), 0, 0);
                    }

                    Task.Delay(3000 / 2).Wait();

                    //ClearLED
                    for (byte i = 0; i < 8; i++)
                    {
                        display.setLED(TM1638.TM1638_LED_COLOR.OFF, i);
                    }

                    display.clearDisplay();
                }
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            _GpioController = GpioController.GetDefault();
            _resetEvent = new ManualResetEvent(false);

            if (this._GpioController != null)
            {

                using (GpioPin data = _GpioController.OpenPin(17))
                using (GpioPin clock = _GpioController.OpenPin(27))
                using (GpioPin strobe = _GpioController.OpenPin(22))
                {
                    data.SetDriveMode(GpioPinDriveMode.Output);
                    clock.SetDriveMode(GpioPinDriveMode.Output);
                    strobe.SetDriveMode(GpioPinDriveMode.Output);

                    var display = new TM1638(data, clock, strobe, true, 1);
                    display.setLED(TM1638.TM1638_LED_COLOR.GREEN, 0);
                    display.setLED(TM1638.TM1638_LED_COLOR.RED, 1);
                    display.setLED(TM1638.TM1638_LED_COLOR.ORANGE, 2);
                }
            }
        }

        private void clear_Click(object sender, RoutedEventArgs e)
        {
            _GpioController = GpioController.GetDefault();
            _resetEvent = new ManualResetEvent(false);

            if (this._GpioController != null)
            {

                using (GpioPin data = _GpioController.OpenPin(17))
                using (GpioPin clock = _GpioController.OpenPin(27))
                using (GpioPin strobe = _GpioController.OpenPin(22))
                {
                    data.SetDriveMode(GpioPinDriveMode.Output);
                    clock.SetDriveMode(GpioPinDriveMode.Output);
                    strobe.SetDriveMode(GpioPinDriveMode.Output);

                    var display = new TM1638(data, clock, strobe, true, 1);
                    //ClearLED
                    for (byte i = 0; i < 8; i++)
                    {
                        display.setLED(TM1638.TM1638_LED_COLOR.OFF, i);
                    }

                    display.clearDisplay();

                }

            }
        }
    }
}
