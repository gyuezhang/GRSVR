using System;
using System.ServiceProcess;
using SuperSocket.SocketBase;
using SuperSocket.SocketEngine;

namespace WinService
{
    public partial class GRService : ServiceBase
    {
        public GRService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            bootstrap = BootstrapFactory.CreateBootstrap();

            if (!bootstrap.Initialize())
            {
                return;
            }

            var result = bootstrap.Start();
            
            if (result == StartResult.Failed)
            {
                return;
            }

        }

        protected override void OnStop()
        {
            //Stop the appServer
            if(bootstrap != null)
                bootstrap.Stop();
        }

        private IBootstrap bootstrap = null;
    }
}
