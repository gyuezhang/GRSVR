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
    public class GetEntWells : CommandBase<GRSession, StringRequestInfo>
    {
        public override string Name
        {
            get { return "GetEntWells"; }
        }

        public override void ExecuteCommand(GRSession session, StringRequestInfo requestInfo)
        {
            string filter = JsonConvert.DeserializeObject<string>(string.Join("", requestInfo.Parameters));

            Tuple<bool, List<C_EntWell>, string> dbRes = C_TabEntWell.Get(filter);

            if (dbRes.Item1)
            {
                session.Send(API_ID.GetEntWells, RES_STATE.OK, JsonConvert.SerializeObject(dbRes.Item2), null);
            }
            else
            {
                session.Send(API_ID.GetEntWells, RES_STATE.FAILED, null, dbRes.Item3);
            }
        }
    }
}