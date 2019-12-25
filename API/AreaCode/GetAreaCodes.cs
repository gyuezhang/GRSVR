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
                C_BdAreaCode bdAc = new C_BdAreaCode(dbRes.Item2);
                session.Send(API_ID.GetAreaCodes, RES_STATE.OK, JsonConvert.SerializeObject(bdAc), null);
            }
            else
            {
                session.Send(API_ID.GetAreaCodes, RES_STATE.FAILED, null, dbRes.Item3);
            }
        }
    }
}