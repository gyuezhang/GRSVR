using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using Newtonsoft.Json;
using GRDB;
using System;
using GRUtil;
using GRModel;

namespace GRSVR
{
    public class DelWellPara : CommandBase<GRSession, StringRequestInfo>
    {
        public override string Name
        {
            get { return "DelWellPara"; }
        }

        public override void ExecuteCommand(GRSession session, StringRequestInfo requestInfo)
        {
            C_WellPara wp = JsonConvert.DeserializeObject<C_WellPara>(string.Join("", requestInfo.Parameters));
            Tuple<bool, string> dbRes = C_TabWellPara.Del(wp);

            if (dbRes.Item1)
            {
                session.Send(API_ID.DelWellPara, RES_STATE.OK, null, null);
            }
            else
            {
                session.Send(API_ID.DelWellPara, RES_STATE.FAILED, null, dbRes.Item2);
            }
        }
    }
}
