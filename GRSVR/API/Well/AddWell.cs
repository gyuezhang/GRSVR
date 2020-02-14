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
    public class AddWell : CommandBase<GRSession, StringRequestInfo>
    {
        public override string Name
        {
            get { return "AddWell"; }
        }

        public override void ExecuteCommand(GRSession session, StringRequestInfo requestInfo)
        {
            List<C_Well> well = JsonConvert.DeserializeObject<List<C_Well>>(string.Join("", requestInfo.Parameters));
            Tuple<bool, string> dbRes = C_TabWell.Add(well);

            if (dbRes.Item1)
            {
                session.Send(E_ApiId.AddWell, E_ResState.OK, null, null);
            }
            else
            {
                session.Send(E_ApiId.AddWell, E_ResState.FAILED, null, dbRes.Item2);
            }
        }
    }
}
