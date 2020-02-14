﻿using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using Newtonsoft.Json;
using GRDb;
using System;
using GRUtil;
using GRModel;
using System.Collections.Generic;

namespace GRSVR
{
    public class EdtWellPara : CommandBase<GRSession, StringRequestInfo>
    {
        public override string Name
        {
            get { return "EdtWellPara"; }
        }

        public override void ExecuteCommand(GRSession session, StringRequestInfo requestInfo)
        {
            List<C_WellPara> wp = JsonConvert.DeserializeObject<List<C_WellPara>>(string.Join("", requestInfo.Parameters));
            Tuple<bool, string> dbRes = C_TabWellPara.Edt(wp[0],wp[1]);

            if (dbRes.Item1)
            {
                session.Send(E_ApiId.EdtWellPara, E_ResState.OK, null, null);
            }
            else
            {
                session.Send(E_ApiId.EdtWellPara, E_ResState.FAILED, null, dbRes.Item2);
            }
        }
    }
}
