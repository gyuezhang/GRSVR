using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using Newtonsoft.Json;
using GRDB;
using System;
using GRUtil;

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
                session.Send(API_ID.Login, RES_STATE.OK, JsonConvert.SerializeObject(C_TabUser.GetUser(session.userId)), null);
            }
            else
            {
                session.Send(API_ID.Login, RES_STATE.FAILED, null, "UserName Or Pwd Error");
            }
        }
    }
}
