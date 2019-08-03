using Ample.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ample.Controls
{
    public class RouteController
    {
        private ApiServer.Api api = new ApiServer.Api();

        public void DoRoute(Client client, string address, byte[] data)
        {
            if (address == nameof(ApiServer.Api.Register))
            {
                api.Register(client, Encoding.ASCII.GetString(data));
            }
            else
            {
                // 
            }
        }
    }
}
