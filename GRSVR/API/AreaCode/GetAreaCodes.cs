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
    public class GetAreaCodes : CommandBase<GRSession, StringRequestInfo>
    {
        public override string Name
        {
            get { return "GetAreaCodes"; }
        }

        public override void ExecuteCommand(GRSession session, StringRequestInfo requestInfo)
        {
            Tuple<bool, List<C_AreaCode>, string> dbRes = C_TabAreaCode.Get();

            if (dbRes.Item1)
            {
                session.Send(E_ApiId.GetAreaCodes, E_ResState.OK, JsonConvert.SerializeObject(dbRes.Item2), null);
            }
            else
            {
                session.Send(E_ApiId.GetAreaCodes, E_ResState.FAILED, null, dbRes.Item3);
            }
        }
    }
}