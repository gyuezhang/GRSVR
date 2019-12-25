using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using Newtonsoft.Json;
using GRDB;
using System;
using GRUtil;

namespace GRSVR
{
    public class AdminLogin : CommandBase<GRSession, StringRequestInfo>
    {
        public override string Name
        {
            get { return "AdminLogin"; }
        }

        public override void ExecuteCommand(GRSession session, StringRequestInfo requestInfo)
        {
            session.userId = 0;

            string pwd = JsonConvert.DeserializeObject<string>(string.Join("", requestInfo.Parameters));
            Tuple<bool, string> loginRes = C_TabAdminPwd.Login(pwd);

            if (loginRes.Item1)
            {
                session.Send(API_ID.AdminLogin, RES_STATE.OK, null, null);
            }
            else
            {
                session.Send(API_ID.AdminLogin, RES_STATE.FAILED, null, loginRes.Item2);
            }
        }
    }
}
