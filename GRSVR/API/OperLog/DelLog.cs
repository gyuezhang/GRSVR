using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using Newtonsoft.Json;
using GRDB;
using System;
using GRUtil;
using GRModel;

namespace GRSVR
{
    public class DelLog : CommandBase<GRSession, StringRequestInfo>
    {
        public override string Name
        {
            get { return "DelLog"; }
        }

        public override void ExecuteCommand(GRSession session, StringRequestInfo requestInfo)
        {
            int id = JsonConvert.DeserializeObject<int>(string.Join("", requestInfo.Parameters));
            Tuple<bool, string> dbRes = C_TabLog.Del(id);

            if (dbRes.Item1)
            {
                session.Send(API_ID.DelLog, RES_STATE.OK, null, null);
            }
            else
            {
                session.Send(API_ID.DelLog, RES_STATE.FAILED, null, dbRes.Item2);
            }
        }
    }
}
