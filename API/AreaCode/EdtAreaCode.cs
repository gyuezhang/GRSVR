using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using Newtonsoft.Json;
using GRDB;
using System;
using GRUtil;
using GRModel;

namespace GRSVR
{
    public class EdtAreaCode : CommandBase<GRSession, StringRequestInfo>
    {
        public override string Name
        {
            get { return "EdtAreaCode"; }
        }

        public override void ExecuteCommand(GRSession session, StringRequestInfo requestInfo)
        {
            C_AreaCode ac = JsonConvert.DeserializeObject<C_AreaCode>(string.Join("", requestInfo.Parameters));
            Tuple<bool, string> dbRes = C_TabAreaCode.Edt(ac);

            if (dbRes.Item1)
            {
                session.Send(API_ID.EdtAreaCode, RES_STATE.OK, null, null);
            }
            else
            {
                session.Send(API_ID.EdtAreaCode, RES_STATE.FAILED, null, dbRes.Item2);
            }
        }
    }
}
