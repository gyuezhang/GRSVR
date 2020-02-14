using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using Newtonsoft.Json;
using GRDb;
using System;
using GRUtil;
using GRModel;

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
            Tuple<bool, string> dbRes = C_TabAdminPwd.Login(pwd);

            if (dbRes.Item1)
            {
                session.Send(E_ApiId.AdminLogin, E_ResState.OK, null, null);
            }
            else
            {
                session.Send(E_ApiId.AdminLogin, E_ResState.FAILED, null, dbRes.Item2);
            }
        }
    }
}
