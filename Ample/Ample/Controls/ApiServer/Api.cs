using Ample.Controls.ApiClient.Models;
using Ample.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ample.Controls.ApiServer
{
    public class Api
    {
        public void Register(Client client, string data)
        {
            try
            {
                var obj = JsonConvert.DeserializeObject<Register>(data);

                // Success
                client.IsRegister = true;
                client.Name = obj.Name;
                client.Permision = Models.Enums.ClientStatus.Register;
                client.ApiVersion = obj.ApiVersion;
            }
            catch (Exception)
            {
                //TODO Сделать отказ
            }
        }
    }
}
