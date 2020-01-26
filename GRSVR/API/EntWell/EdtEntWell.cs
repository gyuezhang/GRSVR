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
    public class EdtEntWell : CommandBase<GRSession, StringRequestInfo>
    {
        public override string Name
        {
            get { return "EdtEntWell"; }
        }

        public override void ExecuteCommand(GRSession session, StringRequestInfo requestInfo)
        {
            C_EntWell well = JsonConvert.DeserializeObject<C_EntWell>(string.Join("", requestInfo.Parameters));
            Tuple<bool, string> dbRes = C_TabEntWell.Edt(well);

            if (dbRes.Item1)
            {
                session.Send(E_ApiId.EdtEntWell, E_ResState.OK, null, null);
            }
            else
            {
                session.Send(E_ApiId.EdtEntWell, E_ResState.FAILED, null, dbRes.Item2);
            }
        }
    }
}
