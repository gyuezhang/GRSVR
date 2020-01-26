using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using Newtonsoft.Json;
using GRDb;
using System;
using GRUtil;
using GRModel;

namespace GRSVR
{
    public class DelUser : CommandBase<GRSession, StringRequestInfo>
    {
        public override string Name
        {
            get { return "DelUser"; }
        }

        public override void ExecuteCommand(GRSession session, StringRequestInfo requestInfo)
        {
            int id = JsonConvert.DeserializeObject<int>(string.Join("", requestInfo.Parameters));
            Tuple<bool, string> dbRes = C_TabUser.Del(id);

            if (dbRes.Item1)
            {
                session.Send(E_ApiId.DelUser, E_ResState.OK, null, null);
            }
            else
            {
                session.Send(E_ApiId.DelUser, E_ResState.FAILED, null, dbRes.Item2);
            }
        }
    }
}
