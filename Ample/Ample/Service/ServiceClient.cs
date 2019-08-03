using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Ample.Service
{
    public class ServiceClient
    {
        private TcpClient client;

        public Task<bool> Connect(string host)
        {
            var res = new TaskCompletionSource<bool>();

            client = new TcpClient();
            client.ReceiveTimeout = 5000;
            client.SendTimeout = 5000;
            try
            {
                var s = client.BeginConnect(host, 8001, (a) => 
                {
                    //TODO Success connect

                }, null);

                //isSuccess = s.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(5));
                Device.StartTimer(TimeSpan.FromSeconds(5), () =>
                {
                    // Result connection
                    bool conn = client.Connected;
                    if (!conn)
                    {
                        client.Close();
                    }
                    res.SetResult(conn);
                    return false;
                });
            }
            catch (Exception)
            {
                res.SetResult(false);
            }

            return res.Task;
        }
    }
}
