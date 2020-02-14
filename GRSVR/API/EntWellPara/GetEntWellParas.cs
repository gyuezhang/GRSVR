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
    public class GetEntWellParas : CommandBase<GRSession, StringRequestInfo>
    {
        public override string Name
        {
            get { return "GetEntWellParas"; }
        }

        public override void ExecuteCommand(GRSession session, StringRequestInfo requestInfo)
        {
            Tuple<bool, C_WellParas, string> dbRes = C_TabEntWellPara.Get();

            if (dbRes.Item1)
            {
                session.Send(E_ApiId.GetEntWellParas, E_ResState.OK, JsonConvert.SerializeObject(dbRes.Item2), null);
            }
            else
            {
                session.Send(E_ApiId.GetEntWellParas, E_ResState.FAILED, null, dbRes.Item3);
            }
        }
    }
}