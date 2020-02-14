using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using Newtonsoft.Json;
using GRDb;
using System;
using GRUtil;
using GRModel;
using System.Collections.Generic;

namespace GRSVR
{
    public class GetUsers : CommandBase<GRSession, StringRequestInfo>
    {
        public override string Name
        {
            get { return "GetUsers"; }
        }

        public override void ExecuteCommand(GRSession session, StringRequestInfo requestInfo)
        {
            Tuple<bool, List<C_User>, string> dbRes = C_TabUser.Get();

            if (dbRes.Item1)
            {
                session.Send(E_ApiId.GetUsers, E_ResState.OK, JsonConvert.SerializeObject(dbRes.Item2), null);
            }
            else
            {
                session.Send(E_ApiId.GetUsers, E_ResState.FAILED, null, dbRes.Item3);
            }
        }
    }
}