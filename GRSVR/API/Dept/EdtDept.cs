﻿using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using Newtonsoft.Json;
using GRDB;
using System;
using GRUtil;
using GRModel;

namespace GRSVR
{
    public class EdtDept : CommandBase<GRSession, StringRequestInfo>
    {
        public override string Name
        {
            get { return "EdtDept"; }
        }

        public override void ExecuteCommand(GRSession session, StringRequestInfo requestInfo)
        {
            Tuple<string, string> edtDeptInputs = JsonConvert.DeserializeObject<Tuple<string, string>>(string.Join("", requestInfo.Parameters));
            Tuple<bool, string> dbRes = C_TabDept.Edt(edtDeptInputs.Item1, edtDeptInputs.Item2);

            if (dbRes.Item1)
            {
                session.Send(API_ID.EdtDept, RES_STATE.OK, null, null);
            }
            else
            {
                session.Send(API_ID.EdtDept, RES_STATE.FAILED, null, dbRes.Item2);
            }
        }
    }
}