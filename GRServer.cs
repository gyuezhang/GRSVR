using GRUtil;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;
using SuperSocket.SocketBase.Protocol;
using System.Collections.Generic;
using System.Text;
using GRModel;
using Newtonsoft.Json;
using GRDB;

namespace GRSVR
{
    public class GRServer : AppServer<GRSession>
    {
        public GRServer()
            : base(new TerminatorReceiveFilterFactory("\r\n", Encoding.UTF8))
        {
            C_Db.ConnDbSvr("localhost", 3306, "root", "123456");
        }

        protected override bool Setup(IRootConfig rootConfig, IServerConfig config)
        {
            return base.Setup(rootConfig, config);
        }

    }

    public class GRSession : AppSession<GRSession>
    {
        public const string RESTMNT = "<RESTMNT>";
        public int userId = -1;

        public void Send(API_ID api_id, RES_STATE res, string json)
        {
            string strMsg;
            if (json == null || json.Length == 0)
                strMsg = api_id.ToString() + " " + res.ToString();
            else
                strMsg = api_id.ToString() + " " + res.ToString() + " " + json;
            strMsg += RESTMNT;
            base.Send(strMsg);
        }

        protected override void HandleUnknownRequest(StringRequestInfo requestInfo)
        {
            base.HandleUnknownRequest(requestInfo);
        }

        protected override void OnSessionStarted()
        {
            this.Send(API_ID.Conn, RES_STATE.OK, null);
        }
    }
}
