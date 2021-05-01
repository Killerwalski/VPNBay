using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace VPNBay.Service
{
    public partial class IPMonitor : ServiceBase
    {
        private readonly ILogger Log = Serilog.Log.ForContext<IPMonitor>();
        public IPMonitor()
        {
            InitializeComponent();
            Log = new LoggerConfiguration()
            .WriteTo.File("C:\\VPNBay.log", rollingInterval: RollingInterval.Day)
            .CreateLogger();
        }

        protected override void OnStart(string[] args)
        {
            Log.Information("VPN Bay Service has been started.");

            SaveCurrentNetworkSnapshot();
            NetworkChange.NetworkAddressChanged += new
            NetworkAddressChangedEventHandler(AddressChangedCallback);
            Log.Information("Listening for address changes...");
        }

        private void SaveCurrentNetworkSnapshot()
        {
            
        }

        protected void AddressChangedCallback(object sender, EventArgs e)
        {
            Log.Information("Network Address changed");

            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface n in adapters)
            {
                // Log.Information("   {0} is {1}", n.Name, n.OperationalStatus);
            }
        }

        protected void KillTasks()
        {

        }

        protected override void OnStop()
        {
            Log.Information("VPN Bay Service has been stopped.");
        }
    }
}
