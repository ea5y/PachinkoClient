//================================
//===Author: easy
//===Email: gopiny@live.com
//===Date: 2017-09-11 09:28
//================================

using Easy.FrameUnity.ESNetwork;
using System;
using UnityEngine;

namespace Asobimo.Pachinko
{
    public class PanelGame : PanelBase<PanelGame>
    {
        public UILabel LblTimes;
        public UILabel LblSum;
        public UILabel LblPbChange;
        public UILabel LblAward;

        public UILabel LblParticipantNum;
        public UILabel LblPlayerState;

        public UILabel LblCurLevel;
        public UIButton BtnUp;
        public UIButton BtnDown;
        public UIButton BtnToggle;
        public UIButton BtnSwitch;

        public UIWidget CtStatus;
        public UIWidget CtOperation;
        public GameObject GroupOperation;

        public UISprite ImgLeft;
        public UISprite ImgRight;

        private MachineData _machineData;

        public override void Back()
        {
            _machineData.Pachinko.Exit();
        }

        public override void Close()
        {
            throw new NotImplementedException();
        }

        public override void Open(object data)
        {
            _machineData = data as MachineData;
            this.Init();
            PanelManager.Open(this.gameObject);
            this.SetCurPacinkoState(_machineData.Data.StateType);
        }

        private void Awake()
        {
            base.Awake();
            this.RegisterBtnEvent();
            this.gameObject.SetActive(false);
        }

        private void Init()
        {
            this.LblTimes.text = _machineData.Data.Times.ToString();
            this.LblSum.text = _machineData.Data.Sum.ToString();
            this.LblPbChange.text = _machineData.Data.PbChange.ToString();
            this.LblAward.text = _machineData.Data.Award.ToString();

            this.LblCurLevel.text = "0";
        }

        private void RegisterBtnEvent()
        {
            EventDelegate.Add(this.BtnUp.onClick, ()=>{});
            EventDelegate.Add(this.BtnDown.onClick, ()=>{});
            EventDelegate.Add(this.BtnToggle.onClick, ()=>{
                    this.OnBtnToggleClick();
                    });
            EventDelegate.Add(this.BtnSwitch.onClick, ()=>{
                    this.OnBtnSwitchClick();
                    });
        }

        private void OnEnable()
        {
            this.RegisterPlayerEvent();
        }

        private void OnDisable()
        {
            this.RemovePlayerEvent();
        }

        private void RegisterPlayerEvent()
        {
            Player.Inst.OnBallsNumChanged += OnPlayerBallsNumChanged;
            Player.Inst.OnStateChanged += OnPlayerStateChanged;
        }

        private void RemovePlayerEvent()
        {
            Player.Inst.OnBallsNumChanged -= OnPlayerBallsNumChanged;
            Player.Inst.OnStateChanged -= OnPlayerStateChanged;
        }

        private void OnPlayerBallsNumChanged(object sender, PlayerBallsNumArgs args)
        {
            
        }

        private void OnPlayerStateChanged(object sender, PlayerStateArgs args)
        {
            switch(args.State)
            {
                case PlayerStateType.None:
                    break;
                case PlayerStateType.Browsing:
                    this.SetPlayerState("Browsing");
                    this.SetOperation(true);
                    this.ShowOperationParent(false);
                    this.ShowStatus(false);
                    break;
                case PlayerStateType.Watching:
                    this.SetPlayerState("Watching");
                    this.SetOperation(false);
                    this.ShowOperationParent(true);
                    this.ShowStatus(true);
                    break;
                case PlayerStateType.Playing:
                    this.SetPlayerState("Playing");
                    this.SetOperation(true);
                    this.ShowOperationParent(true);
                    this.ShowStatus(true);
                    break;
            }
        }

        public void SetCurPacinkoState(PachinkoStateType type)
        {
            switch(type)
            {
                case PachinkoStateType.Unoccupied:
                    this.SetUnoccupied();
                    break;
                case PachinkoStateType.Occupied:
                    this.SetOccupied();
                    break;
                case PachinkoStateType.Owned:
                    this.SetOwned();
                    break;
                case PachinkoStateType.Maintain:
                    this.SetMaintain();
                    break;
                case PachinkoStateType.Reset:
                    this.SetReset();
                    break;
                case PachinkoStateType.LostConnection:
                    this.SetLostConnection();
                    break;
            }
        }

        private void SetOperation(bool isEnable)
        {
            this.BtnToggle.enabled = isEnable;
            if(isEnable)
            {
                this.BtnToggle.SetState(UIButton.State.Normal, true);
            }
            else
            {
                this.BtnToggle.SetState(UIButton.State.Disabled, true);
                this.ShowOperation(false);
            }
        }

        private void ShowOperationParent(bool isShow)
        {
            this.BtnSwitch.gameObject.SetActive(!isShow);
            this.GroupOperation.SetActive(isShow);
        }

        private void OnBtnToggleClick()
        {
            var isShow = this.CtOperation.gameObject.activeSelf;
            this.ShowOperation(!isShow);
        }

        private void OnBtnSwitchClick()
        {
            this.StartPlayGame();
        }

        private void StartPlayGame()
        {
            Net.StartPlayGame(_machineData.Data.Id);
            _machineData.Pachinko.Start();
        }
        
        public void EndPlayGame()
        {
            _machineData.Pachinko.End();
        }

        private void ShowOperation(bool isShow)
        {
            this.ImgLeft.gameObject.SetActive(!isShow);
            this.ImgRight.gameObject.SetActive(isShow);
            this.CtOperation.gameObject.SetActive(isShow);
        }

        private void SetPlayerState(string state)
        {
            this.LblPlayerState.text = state;
        }

        private void SetParticipantNum(int num)
        {
            this.LblParticipantNum.text = num.ToString();
        }

        private void SetUnoccupied()
        {
            Player.Inst.State = PlayerStateType.Browsing;
            _machineData.Pachinko.SetState(_machineData.Pachinko.UnoccupiedState);
        }

        private void SetOccupied()
        {
            Player.Inst.State = PlayerStateType.Watching;
            _machineData.Pachinko.SetState(_machineData.Pachinko.OccupiedState);
        }

        private void SetOwned()
        {
            Player.Inst.State = PlayerStateType.Playing;
            _machineData.Pachinko.SetState(_machineData.Pachinko.OwnedState);
        }

        private void SetMaintain()
        {
            //@TODO To panel main
            _machineData.Pachinko.SetState(_machineData.Pachinko.MaintainState);
        }

        private void SetReset()
        {
            //@TODO Show reset time
            //if finish
            this.SetCurPacinkoState(PachinkoStateType.Unoccupied);
        }

        private void SetLostConnection()
        {
            //@TODO SHow LostConnection time
            //if finish
            this.SetCurPacinkoState(PachinkoStateType.Unoccupied);
        }

        public void ShowStatus()
        {
            var active = this.CtStatus.gameObject.activeSelf;
            //this.CtStatus.gameObject.SetActive(!active);
            this.ShowStatus(!active);
        }

        public void ShowStatus(bool isShow)
        {
            this.CtStatus.gameObject.SetActive(isShow);
        }
    }
}
