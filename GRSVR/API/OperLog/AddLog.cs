using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using Newtonsoft.Json;
using GRDB;
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
                session.Send(API_ID.AddLog, RES_STATE.OK, null, null);
            }
            else
            {
                session.Send(API_ID.AddLog, RES_STATE.FAILED, null, dbRes.Item2);
            }
        }
    }
}
