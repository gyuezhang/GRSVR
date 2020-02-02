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
    public class GetLogs : CommandBase<GRSession, StringRequestInfo>
    {
        public override string Name
        {
            get { return "GetLogs"; }
        }

        public override void ExecuteCommand(GRSession session, StringRequestInfo requestInfo)
        {
            Tuple<bool, List<C_Log>, string> dbRes = C_TabLog.Get();

            if (dbRes.Item1)
            {
                session.Send(API_ID.GetLogs, RES_STATE.OK, JsonConvert.SerializeObject(dbRes.Item2), null);
            }
            else
            {
                session.Send(API_ID.GetLogs, RES_STATE.FAILED, null, dbRes.Item3);
            }
        }
    }
}