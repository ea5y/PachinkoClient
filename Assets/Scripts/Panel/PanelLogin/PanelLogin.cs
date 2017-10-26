//================================
//===Author: easy
//===Email: gopiny@live.com
//===Date: 2017-09-25 10:23
//================================

using UnityEngine;
using System;
using Easy.FrameUnity.Net;

namespace Asobimo.Pachinko
{
    public class PanelLogin : MonoBehaviour
    {
        public UIInput Username;
        public UIInput Password;
        public UIButton BtnLogin;

        private void Awake()
        {
            this.RegisterBtnEvent();
        }

        public void RegisterBtnEvent()
        {
            EventDelegate.Add(this.BtnLogin.onClick, ()=>{
                    this.OnBtnLoginClick();
                    });
        }

        public void OnBtnLoginClick()
        {
            Net.Login(Username.value, Password.value, (res)=>{
                    Player.Inst.BallsNum = res.UserData.BallsNum;
                    PackageReqHead.SessionId = res.SessionId;
                    SocketClient.CreateHeartbeatTimer();
                //ScenesManager.Inst.EnterScene(ScenesName.E_SceneGame_1);
                ScenesManager.Inst.EnterLoadingScene(SceneName.E_SceneGame_1);
                    });
        }
    }
}
