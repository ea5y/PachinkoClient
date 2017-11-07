//================================
//===Author: easy
//===Email: gopiny@live.com
//===Date: 2017-09-25 10:23
//================================

using UnityEngine;
using Easy.FrameUnity.ESNetwork;
using Easy.FrameUnity.Util;

namespace Asobimo.Pachinko
{
    public class PanelLogin : MonoBehaviour
    {
        public UIInput Username;
        public UIInput Password;
        public UIButton BtnLogin;

        private RegisterDataReq _data;

        private void Awake()
        {
            this.RegisterBtnEvent();
            this.LoadLoginConfig();
            this.Init();
        }

        private void LoadLoginConfig()
        {
            _data = IOHelperUtil.ReadFromJson<RegisterDataReq>(URL.DEBUG_CONFIG);
        }

        private void Init()
        {
            this.Username.value = _data.Username;
            this.Password.value = _data.Password;
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
					NetPackage.Sid = res.SessionId;
					Net.Heartbeat();
                    ScenesManager.Inst.EnterLoadingScene(SceneName.E_SceneGame_1);
                    });
        }
    }
}
