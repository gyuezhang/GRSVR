using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using Newtonsoft.Json;
using GRDb;
using System;
using GRUtil;
using GRModel;

namespace GRSVR
{
    public class DelWell : CommandBase<GRSession, StringRequestInfo>
    {
        public override string Name
        {
            get { return "DelWell"; }
        }

        public override void ExecuteCommand(GRSession session, StringRequestInfo requestInfo)
        {
            int id = JsonConvert.DeserializeObject<int>(string.Join("", requestInfo.Parameters));
            Tuple<bool, string> dbRes = C_TabWell.Del(id);

            if (dbRes.Item1)
            {
                session.Send(E_ApiId.DelWell, E_ResState.OK, null, null);
            }
            else
            {
                session.Send(E_ApiId.DelWell, E_ResState.FAILED, null, dbRes.Item2);
            }
        }
    }
}
