﻿using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using Newtonsoft.Json;
using GRDb;
using System;
using GRUtil;
using GRModel;

namespace GRSVR
{
    public class AddWellPara : CommandBase<GRSession, StringRequestInfo>
    {
        public override string Name
        {
            get { return "AddWellPara"; }
        }

        public override void ExecuteCommand(GRSession session, StringRequestInfo requestInfo)
        {
            C_WellPara wp = JsonConvert.DeserializeObject<C_WellPara>(string.Join("", requestInfo.Parameters));
            Tuple<bool, string> dbRes = C_TabWellPara.Add(wp);

            if (dbRes.Item1)
            {
                session.Send(E_ApiId.AddWellPara, E_ResState.OK, null, null);
            }
            else
            {
                session.Send(E_ApiId.AddWellPara, E_ResState.FAILED, null, dbRes.Item2);
            }
        }
    }
}
