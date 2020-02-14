using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using Newtonsoft.Json;
using GRDb;
using System;
using GRUtil;
using GRModel;

namespace GRSVR
{
    public class AddAnm : CommandBase<GRSession, StringRequestInfo>
    {
        public override string Name
        {
            get { return "AddAnm"; }
        }

        public override void ExecuteCommand(GRSession session, StringRequestInfo requestInfo)
        {
            C_Anm a = JsonConvert.DeserializeObject<C_Anm>(string.Join("", requestInfo.Parameters));
            Tuple<bool, string> dbRes = C_TabAnm.Add(a);

            if (dbRes.Item1)
            {
                session.Send(E_ApiId.AddAnm, E_ResState.OK, null, null);
            }
            else
            {
                session.Send(E_ApiId.AddAnm, E_ResState.FAILED, null, dbRes.Item2);
            }
        }
    }
}
