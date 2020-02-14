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
    public class GetEntWells : CommandBase<GRSession, StringRequestInfo>
    {
        public override string Name
        {
            get { return "GetEntWells"; }
        }

        public override void ExecuteCommand(GRSession session, StringRequestInfo requestInfo)
        {
            C_SearchCondition filter = JsonConvert.DeserializeObject<C_SearchCondition>(string.Join("", requestInfo.Parameters));

            Tuple<bool, List<C_EntWell>, string> dbRes = C_TabEntWell.Get(filter);

            if (dbRes.Item1)
            {
                session.Send(E_ApiId.GetEntWells, E_ResState.OK, JsonConvert.SerializeObject(dbRes.Item2), null);
            }
            else
            {
                session.Send(E_ApiId.GetEntWells, E_ResState.FAILED, null, dbRes.Item3);
            }
        }
    }
}