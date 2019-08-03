using Ample.Core;
using Ample.Models;
using Ample.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Xamarin.Forms;

namespace Ample.Service
{
    public class ServiceServer
    {
        public bool IsRunning { get; set; }
        public List<Client> Clients { get; set; } = new List<Client>();
        public Thread RegisterThread { get; set; }
        public TcpListener Listener { get; set; }

        public void Start()
        {
            if (IsRunning) return;

            IsRunning = true;

            if (Device.RuntimePlatform == Device.UWP)
            {

            }
            else
            {
                // Implement sockets
                Listener = new TcpListener(IPAddress.Any, 8008);
                Listener.Start();

                if (RegisterThread == null)
                {
                    RegisterThread = new Thread(RegisterEngine);
                }
                RegisterThread.Start();
            }

            DependencyService.Get<ICrossPlatform>().Echo("Server starting");
        }

        public void ShutDown()
        {
            IsRunning = false;
            RegisterThread.Abort();
        }

        private void RegisterEngine()
        {
            try
            {
                while (true)
                {
                    var tcp = Listener.AcceptTcpClient();
                    DependencyService.Get<ICrossPlatform>().Echo("Connected!");

                    Clients.Add(new Client
                    {
                        Tcp = tcp,
                    });
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
