using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using Newtonsoft.Json;
using GRDb;
using System;
using GRUtil;
using GRModel;

namespace GRSVR
{
    public class AddUser : CommandBase<GRSession, StringRequestInfo>
    {
        public override string Name
        {
            get { return "AddUser"; }
        }

        public override void ExecuteCommand(GRSession session, StringRequestInfo requestInfo)
        {
            C_User user = JsonConvert.DeserializeObject<C_User>(string.Join("", requestInfo.Parameters));
            Tuple<bool, string> dbRes = C_TabUser.Add(user);

            if (dbRes.Item1)
            {
                session.Send(E_ApiId.AddUser, E_ResState.OK, null, null);
            }
            else
            {
                session.Send(E_ApiId.AddUser, E_ResState.FAILED, null, dbRes.Item2);
            }
        }
    }
}
