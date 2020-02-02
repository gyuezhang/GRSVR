using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using Newtonsoft.Json;
using GRDb;
using System;
using GRUtil;
using GRModel;

namespace GRSVR
{
    public class AddLog : CommandBase<GRSession, StringRequestInfo>
    {
        public override string Name
        {
            get { return "AddLog"; }
        }

        public override void ExecuteCommand(GRSession session, StringRequestInfo requestInfo)
        {
            C_Log log = JsonConvert.DeserializeObject<C_Log>(string.Join("", requestInfo.Parameters));
            Tuple<bool, string> dbRes = C_TabLog.Add(log);

            if (dbRes.Item1)
            {
                session.Send(E_ApiId.AddLog, E_ResState.OK, null, null);
            }
            else
            {
                session.Send(E_ApiId.AddLog, E_ResState.FAILED, null, dbRes.Item2);
            }
        }
    }
}
