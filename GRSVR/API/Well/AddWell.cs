using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using Newtonsoft.Json;
using GRDB;
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
                session.Send(API_ID.AddWell, RES_STATE.OK, null, null);
            }
            else
            {
                session.Send(API_ID.AddWell, RES_STATE.FAILED, null, dbRes.Item2);
            }
        }
    }
}
