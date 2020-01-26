using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using Newtonsoft.Json;
using GRDb;
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
                session.Send(E_ApiId.EdtAreaCode, E_ResState.OK, null, null);
            }
            else
            {
                session.Send(E_ApiId.EdtAreaCode, E_ResState.FAILED, null, dbRes.Item2);
            }
        }
    }
}
