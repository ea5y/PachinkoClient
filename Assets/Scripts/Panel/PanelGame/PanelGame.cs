//================================
//===Author: easy
//===Email: gopiny@live.com
//===Date: 2017-09-11 09:28
//================================

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
        public UIButton BtnHide;

        public UIWidget CtStatus;
        public UIWidget CtOperation;

        public UISprite ImgLeft;
        public UISprite ImgRight;

        private MachineData _machineData;

        public override void Back()
        {
            throw new NotImplementedException();
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
            EventDelegate.Add(this.BtnHide.onClick, ()=>{
                    var active = this.CtOperation.gameObject.activeSelf;
                    this.SetOperation(!active);
                    });
        }

        private void SetCurPacinkoState(PachinkoStateType type)
        {
            switch(type)
            {
                case PachinkoStateType.Unoccupied:
                    this.SetUnoccupied();
                    break;
                case PachinkoStateType.Occupied:
                    this.SetOccupied();
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
            this.SetBtnHide(!isEnable);
            this.ShowOperation();
            if(isEnable)
            {
                //NO hide
            }
            else
            {
                //Hide
            }
        }

        private void ShowOperation()
        {
            var active = this.CtOperation.gameObject.activeSelf;
            this.CtOperation.gameObject.SetActive(!active);
        }

        private void SetBtnHide(bool isHide)
        {
            this.ImgLeft.gameObject.SetActive(isHide);
            this.ImgRight.gameObject.SetActive(!isHide);
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
            this.SetPlayerState("Browsing");
            this.SetOperation(true);
        }

        private void SetOccupied()
        {
            this.SetPlayerState("Waching");
            this.SetOperation(false);
        }

        private void SetMaintain()
        {
            //@TODO To panel main
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
            this.CtStatus.gameObject.SetActive(!active);
        }
    }
}

