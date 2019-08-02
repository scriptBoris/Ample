using Ample.Core;
using Ample.Models;
using Ample.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Ample.Service
{
    public class ServiceServer
    {
        public bool IsRunning { get; set; }

        public void Start()
        {
            if (IsRunning) return;

            IsRunning = true;
        }

        public void ShutDown()
        {
            IsRunning = false;
        }
    }
}
