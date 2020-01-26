using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using Newtonsoft.Json;
using GRDb;
using System;
using GRUtil;
using GRModel;

namespace GRSVR
{
    public class Login : CommandBase<GRSession, StringRequestInfo>
    {
        public override string Name
        {
            get { return "Login"; }
        }

        public override void ExecuteCommand(GRSession session, StringRequestInfo requestInfo)
        {
            Tuple<string, string> loginInputs = JsonConvert.DeserializeObject<Tuple<string, string>>(string.Join("", requestInfo.Parameters));
            
            //get id
            if(C_TabUser.GetId(loginInputs.Item1).Item1)
            {
                session.userId = int.Parse(C_TabUser.GetId(loginInputs.Item1).Item2);
            }

            if (C_TabUser.Login(loginInputs.Item1, loginInputs.Item2))
            {
                session.Send(E_ApiId.Login, E_ResState.OK, JsonConvert.SerializeObject(C_TabUser.GetUser(session.userId).Item2), null);
            }
            else
            {
                session.Send(E_ApiId.Login, E_ResState.FAILED, null, "UserName Or Pwd Error");
            }
        }
    }
}
