using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using Newtonsoft.Json;
using GRDB;
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
                session.Send(API_ID.DelWell, RES_STATE.OK, null, null);
            }
            else
            {
                session.Send(API_ID.DelWell, RES_STATE.FAILED, null, dbRes.Item2);
            }
        }
    }
}
