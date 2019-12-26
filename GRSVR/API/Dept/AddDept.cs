using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using Newtonsoft.Json;
using GRDB;
using System;
using GRUtil;
using GRModel;

namespace GRSVR
{
    public class AddDept : CommandBase<GRSession, StringRequestInfo>
    {
        public override string Name
        {
            get { return "AddDept"; }
        }

        public override void ExecuteCommand(GRSession session, StringRequestInfo requestInfo)
        {
            string deptName = JsonConvert.DeserializeObject<string>(string.Join("", requestInfo.Parameters));
            Tuple<bool, string> dbRes = C_TabDept.Add(deptName);

            if (dbRes.Item1)
            {
                session.Send(API_ID.AddDept, RES_STATE.OK, null, null);
            }
            else
            {
                session.Send(API_ID.AddDept, RES_STATE.FAILED, null, dbRes.Item2);
            }
        }
    }
}
