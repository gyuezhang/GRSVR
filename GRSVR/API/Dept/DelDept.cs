using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using Newtonsoft.Json;
using GRDb;
using System;
using GRUtil;
using GRModel;

namespace GRSVR
{
    public class DelDept : CommandBase<GRSession, StringRequestInfo>
    {
        public override string Name
        {
            get { return "DelDept"; }
        }

        public override void ExecuteCommand(GRSession session, StringRequestInfo requestInfo)
        {
            string deptName = JsonConvert.DeserializeObject<string>(string.Join("", requestInfo.Parameters));
            Tuple<bool, string> dbRes = C_TabDept.Del(deptName);

            if (dbRes.Item1)
            {
                session.Send(E_ApiId.DelDept, E_ResState.OK, null, null);
            }
            else
            {
                session.Send(E_ApiId.DelDept, E_ResState.FAILED, null, dbRes.Item2);
            }
        }
    }
}
