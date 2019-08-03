using Ample.Core;
using Ample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Ample.Controls
{
    public class ClientController
    {

        public void TryRegister(Client client)
        {
            Device.StartTimer(new TimeSpan(0,0,5), () =>
            {
                if (!client.IsRegister)
                {
                    DropClient(client);
                }
                return false;
            });
        }

        public void DropClient(Client client)
        {
            var match = AppServices.Server.Clients.FirstOrDefault(x => x == client);
            if (match != null)
            {
                AppServices.Server.Clients.Remove(match);
            }
            client.Tcp.Close();
        }
    }
}
