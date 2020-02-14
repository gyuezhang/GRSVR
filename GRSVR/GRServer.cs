using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;
using SuperSocket.SocketBase.Protocol;
using System.Text;
using GRModel;
using GRUtil;
using GRDb;

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

        public void Send(E_ApiId apiId, E_ResState res, string json, string exLog)
        {
            //Send
            string strMsg;

            if (json == null || json.Length == 0)
                strMsg = apiId.ToString() + " " + res.ToString();
            else
                strMsg = apiId.ToString() + " " + res.ToString() + " " + json;
            strMsg += RESTMNT;
            base.Send(strMsg);

            //Log
            string strEx; E_LogLevel level;

            if (res == E_ResState.OK)
            {
                strEx = "Success";
                if (userId == 0)
                    level = E_LogLevel.Admin;
                else
                    level = E_LogLevel.User;
            }
            else
            {
                strEx = exLog;
                level = E_LogLevel.Debug;
            }
            if(userId>-1 && Comm.IsApiNeedLog(apiId))
                C_TabLog.Add(new C_Log(userId, apiId, level, strEx));
        }

        protected override void HandleUnknownRequest(StringRequestInfo requestInfo)
        {
            base.HandleUnknownRequest(requestInfo);
        }

        protected override void OnSessionStarted()
        {
            this.Send(E_ApiId.Conn, E_ResState.OK, null, null);
        }
    }
}
