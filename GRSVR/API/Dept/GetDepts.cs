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
    public class GetDepts : CommandBase<GRSession, StringRequestInfo>
    {
        public override string Name
        {
            get { return "GetDepts"; }
        }

        public override void ExecuteCommand(GRSession session, StringRequestInfo requestInfo)
        {
            Tuple<bool, List<string>, string> dbRes = C_TabDept.Get();

            if (dbRes.Item1)
            {
                session.Send(E_ApiId.GetDepts, E_ResState.OK, JsonConvert.SerializeObject(dbRes.Item2), null);
            }
            else
            {
                session.Send(E_ApiId.GetDepts, E_ResState.FAILED, null, dbRes.Item3);
            }
        }
    }
}