using Ample.Controls;
using Ample.Core;
using Ample.Models.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Ample.Models
{
    public class Client
    {
        #region Private
        private Thread dataThread;
        private Stream stream;
        private StreamReader streamRead;
        private StreamWriter streamWriter;
        #endregion

        #region Data
        public ClientStatus Permision { get; set; }
        public bool IsRegister { get; set; }
        public int ApiVersion { get; set; }
        public string Name { get; set; }
        public TcpClient Tcp { get; set; }
        #endregion
        public Client()
        {
            streamRead = new StreamReader(Tcp.GetStream());
            streamWriter = new StreamWriter(Tcp.GetStream());
            stream = Tcp.GetStream();
            dataThread = new Thread(DataListener);
            dataThread.Start();
        }

        public void Destroy()
        {
            dataThread.Abort();
        }

        private void DataListener()
        {
            try
            {
                while (true)
                {
                    byte[] data = new byte[100];
                    int bytes = stream.Read(data, 0, data.Length);

                    // Do known content length
                    var dlength = data.GetBytesByRange(0, 10);
                    string slength = Encoding.ASCII.GetString(dlength);
                    int contentLength = int.Parse(slength);

                    // Do known method address
                    var daddress = data.GetBytesByRange(11, 100);
                    string address = Encoding.ASCII.GetString(daddress);

                    // Get body
                    byte[] dataBody = new byte[contentLength];
                    stream.Read(dataBody, 101, contentLength);


                    AppControllers.Route.DoRoute(this, address, dataBody);
                }
            }
            catch (ThreadAbortException)
            {
            }
            catch (Exception)
            {
            }
        }
    }
}
