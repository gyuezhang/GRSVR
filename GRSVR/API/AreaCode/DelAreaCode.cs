using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using Newtonsoft.Json;
using GRDB;
using System;
using GRUtil;

namespace GRSVR
{
    public class DelAreaCode : CommandBase<GRSession, StringRequestInfo>
    {
        public override string Name
        {
            get { return "DelAreaCode"; }
        }

        public override void ExecuteCommand(GRSession session, StringRequestInfo requestInfo)
        {
            int id = JsonConvert.DeserializeObject<int>(string.Join("", requestInfo.Parameters));
            Tuple<bool, string> dbRes = C_TabAreaCode.Del(id);

            if (dbRes.Item1)
            {
                session.Send(API_ID.DelAreaCode, RES_STATE.OK, null, null);
            }
            else
            {
                session.Send(API_ID.DelAreaCode, RES_STATE.FAILED, null, dbRes.Item2);
            }
        }
    }
}
