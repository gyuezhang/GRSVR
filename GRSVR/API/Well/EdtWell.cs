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
    public class EdtWell : CommandBase<GRSession, StringRequestInfo>
    {
        public override string Name
        {
            get { return "EdtWell"; }
        }

        public override void ExecuteCommand(GRSession session, StringRequestInfo requestInfo)
        {
            C_Well well = JsonConvert.DeserializeObject<C_Well>(string.Join("", requestInfo.Parameters));
            Tuple<bool, string> dbRes = C_TabWell.Edt(well);

            if (dbRes.Item1)
            {
                session.Send(E_ApiId.EdtWell, E_ResState.OK, null, null);
            }
            else
            {
                session.Send(E_ApiId.EdtWell, E_ResState.FAILED, null, dbRes.Item2);
            }
        }
    }
}
