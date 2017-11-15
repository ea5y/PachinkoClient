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
            NetPackage.Sid = _data.Sid;
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
            Net.Login(_data, Password.value, (res)=>{
                    Player.Inst.UserData = res.UserData;
                    //Player.Inst.BallsNum = res.UserData.BallsNum;
					NetPackage.Sid = res.SessionId;
                    _data.Sid = res.SessionId;
                    IOHelperUtil.SaveToJson<RegisterDataReq>(_data, URL.DEBUG_CONFIG);

					Net.Heartbeat();
                    ScenesManager.Inst.EnterLoadingScene(SceneName.E_SceneGame_1);
                    });
        }
    }
}
