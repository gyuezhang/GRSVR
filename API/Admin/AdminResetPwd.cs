using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using Newtonsoft.Json;
using GRDB;
using System;
using GRUtil;

namespace GRSVR
{
    public class AdminResetPwd : CommandBase<GRSession, StringRequestInfo>
    {
        public override string Name
        {
            get { return "AdminResetPwd"; }
        }

        public override void ExecuteCommand(GRSession session, StringRequestInfo requestInfo)
        {
            session.userId = 0;

            Tuple<string, string> resetPwdInputs = JsonConvert.DeserializeObject<Tuple<string, string>>(string.Join("", requestInfo.Parameters));
            Tuple<bool, string> dbRes = C_TabAdminPwd.ResetPwd(resetPwdInputs.Item1, resetPwdInputs.Item2);

            if (dbRes.Item1)
            {
                session.Send(API_ID.AdminResetPwd, RES_STATE.OK, null, null);
            }
            else
            {
                session.Send(API_ID.AdminResetPwd, RES_STATE.FAILED, null, dbRes.Item2);
            }
        }
    }
}
