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
    public class AddEntWell : CommandBase<GRSession, StringRequestInfo>
    {
        public override string Name
        {
            get { return "AddEntWell"; }
        }

        public override void ExecuteCommand(GRSession session, StringRequestInfo requestInfo)
        {
            List<C_EntWell> entWell = JsonConvert.DeserializeObject<List<C_EntWell>>(string.Join("", requestInfo.Parameters));
            Tuple<bool, string> dbRes = C_TabEntWell.Add(entWell);

            if (dbRes.Item1)
            {
                session.Send(E_ApiId.AddEntWell, E_ResState.OK, null, null);
            }
            else
            {
                session.Send(E_ApiId.AddEntWell, E_ResState.FAILED, null, dbRes.Item2);
            }
        }
    }
}
