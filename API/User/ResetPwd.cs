using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using Newtonsoft.Json;
using GRDB;
using System;
using GRUtil;
using GRModel;

namespace GRSVR
{
    public class ResetPwd : CommandBase<GRSession, StringRequestInfo>
    {
        public override string Name
        {
            get { return "ResetPwd"; }
        }

        public override void ExecuteCommand(GRSession session, StringRequestInfo requestInfo)
        {
            Tuple<string, string> resetPwdInputs = JsonConvert.DeserializeObject<Tuple<string, string>>(string.Join("", requestInfo.Parameters));

            //Get User
            C_User curUser;
            if(C_TabUser.GetUser(session.userId).Item1)
            {
                curUser = C_TabUser.GetUser(session.userId).Item2;
            }
            else
            {
                session.Send(API_ID.ResetPwd, RES_STATE.FAILED, null, "Can not find User by UserId " + session.userId.ToString());
                return;
            }

            //Edt User
            if(resetPwdInputs.Item1 == curUser.Pwd)
            {
                curUser.Pwd = resetPwdInputs.Item2;
                Tuple<bool, string> dbRes = C_TabUser.Edt(curUser);

                if(dbRes.Item1)
                    session.Send(API_ID.ResetPwd, RES_STATE.OK, null, null);
                else 
                    session.Send(API_ID.ResetPwd, RES_STATE.FAILED, null, dbRes.Item2);
            }
            else
            {
                session.Send(API_ID.ResetPwd, RES_STATE.FAILED, null, "OldPwd Error");
            }
        }
    }
}
