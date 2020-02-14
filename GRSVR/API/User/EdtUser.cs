using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using Newtonsoft.Json;
using GRDb;
using System;
using GRUtil;
using GRModel;

namespace GRSVR
{
    public class EdtUser : CommandBase<GRSession, StringRequestInfo>
    {
        public override string Name
        {
            get { return "EdtUser"; }
        }

        public override void ExecuteCommand(GRSession session, StringRequestInfo requestInfo)
        {
            C_User user = JsonConvert.DeserializeObject<C_User>(string.Join("", requestInfo.Parameters));
            Tuple<bool, string> dbRes = C_TabUser.Edt(user);

            if (dbRes.Item1)
            {
                session.Send(E_ApiId.EdtUser, E_ResState.OK, JsonConvert.SerializeObject(C_TabUser.GetUser(session.userId).Item2), null);
            }
            else
            {
                session.Send(E_ApiId.EdtUser, E_ResState.FAILED, null, dbRes.Item2);
            }
        }
    }
}
