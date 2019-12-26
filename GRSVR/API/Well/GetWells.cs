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
    public class GetWells : CommandBase<GRSession, StringRequestInfo>
    {
        public override string Name
        {
            get { return "GetWells"; }
        }

        public override void ExecuteCommand(GRSession session, StringRequestInfo requestInfo)
        {
            string filter = JsonConvert.DeserializeObject<string>(string.Join("", requestInfo.Parameters));

            Tuple<bool, List<C_Well>, string> dbRes = C_TabWell.Get(filter);

            if (dbRes.Item1)
            {
                session.Send(API_ID.GetWells, RES_STATE.OK, JsonConvert.SerializeObject(dbRes.Item2), null);
            }
            else
            {
                session.Send(API_ID.GetWells, RES_STATE.FAILED, null, dbRes.Item3);
            }
        }
    }
}