using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using Newtonsoft.Json;
using GRDb;
using System;
using GRUtil;
using GRModel;

namespace GRSVR
{
    public class AddAreaCode : CommandBase<GRSession, StringRequestInfo>
    {
        public override string Name
        {
            get { return "AddAreaCode"; }
        }

        public override void ExecuteCommand(GRSession session, StringRequestInfo requestInfo)
        {
            C_AreaCode ac = JsonConvert.DeserializeObject<C_AreaCode>(string.Join("", requestInfo.Parameters));
            Tuple<bool, string> dbRes = C_TabAreaCode.Add(ac);

            if (dbRes.Item1)
            {
                session.Send(E_ApiId.AddAreaCode, E_ResState.OK, null, null);
            }
            else
            {
                session.Send(E_ApiId.AddAreaCode, E_ResState.FAILED, null, dbRes.Item2);
            }
        }
    }
}
