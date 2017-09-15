//================================
//===Author: easy
//===Email: gopiny@live.com
//===Date: 2017-09-11 11:15
//================================

using System;
using UnityEngine;

namespace Asobimo.Pachinko
{
    public class PanelTopbar : PanelBase<PanelTopbar>
    {
        public UIButton BtnMenu;
        public UIButton BtnBack;
        public UILabel LblBallsNum;

        public UIWidget CtPullDownList;
        public UIGrid Grid;
        public UIButton BtnRest;
        public UIButton BtnAuto;
        public UIButton BtnFinish;
        public UIButton BtnStatus;
        public UIButton BtnShop;
        public UIButton BtnExchange;

        public UIWidget CtStatus;

        public override void Back()
        {
            //this.Home();
        }

        public override void Close()
        {
            throw new NotImplementedException();
        }

        public override void Open(object data)
        {
            throw new NotImplementedException();
        }

        private void Awake()
        {
            base.Awake();
            this.RegisterBtnEvent();
            this.RegisterPlayerEvent();
            Player.Inst.Init();
        }

        private void RegisterPlayerEvent()
        {
            Player.Inst.OnStateChanged += OnPlayerStateChanged;
            Player.Inst.OnBallsNumChanged += OnPlayerBallsNumChanged;
        }

        private void OnPlayerStateChanged(object sender, PlayerStateArgs args)
        {
            this.SetPullDownList(args.State);
        }

        private void SetPullDownList(PlayerStateType state)
        {
            this.ClearPullDownList();
            switch(state)
            {
                case PlayerStateType.None:
                    this.SetBtnMenu(true);
                    this.AddBtnToPullDownList(this.BtnShop.gameObject);
                    this.AddBtnToPullDownList(this.BtnExchange.gameObject);
                    break;
                case PlayerStateType.Browsing:
                    this.SetBtnMenu(false);
                    break;
                case PlayerStateType.Watching:
                    this.SetBtnMenu(false);
                    break;
                case PlayerStateType.Playing:
                    this.SetBtnMenu(true);
                    this.AddBtnToPullDownList(this.BtnAuto.gameObject);
                    this.AddBtnToPullDownList(this.BtnRest.gameObject);
                    this.AddBtnToPullDownList(this.BtnFinish.gameObject);
                    this.AddBtnToPullDownList(this.BtnStatus.gameObject);
                    break;
                case PlayerStateType.Exchange:
                    this.SetBtnMenu(false);
                    break;
            }
        }

        private void SetBtnMenu(bool isMenu)
        {
            this.BtnBack.gameObject.SetActive(!isMenu);
            this.BtnMenu.gameObject.SetActive(isMenu);
        }

        private void AddBtnToPullDownList(GameObject btn)
        {
            btn.SetActive(true);
            var go = NGUITools.AddChild(this.Grid.gameObject, btn);
            this.Grid.repositionNow = true;
        }

        private void ClearPullDownList()
        {
            this.Grid.gameObject.DestroyChild();
        }

        private void OnPlayerBallsNumChanged(object sender, PlayerBallsNumArgs args)
        {
            this.SetBallsNum(args.BallsNum);
        }

        private void SetBallsNum(int num)
        {
            this.LblBallsNum.text = num.ToString();
        }

        private void RegisterBtnEvent()
        {
            EventDelegate.Add(this.BtnMenu.onClick, ()=>{ this.OnBtnMenuClick(); });
            EventDelegate.Add(this.BtnBack.onClick, ()=>{ this.OnBtnBackClick(); });
            EventDelegate.Add(this.BtnRest.onClick, ()=>{ this.OnBtnResetClick(); });
            EventDelegate.Add(this.BtnAuto.onClick, ()=>{ this.OnBtnAutoClick(); });
            EventDelegate.Add(this.BtnFinish.onClick, ()=>{ this.OnBtnFinishClick(); });
            EventDelegate.Add(this.BtnStatus.onClick, ()=>{this.OnBtnStatusClick(); });
            EventDelegate.Add(this.BtnShop.onClick, ()=>{this.OnBtnShopClick(); });
            EventDelegate.Add(this.BtnExchange.onClick, ()=>{this.OnBtnExchangeClick();});
        }

        private void OnBtnMenuClick()
        {
            this.ShowPullDownList();
        }

        private void OnBtnBackClick()
        {
            //PanelGame.Inst.Back();
            PanelManager.Back();
        }

        private void ShowPullDownList()
        {
            var active = this.CtPullDownList.gameObject.activeSelf;
            this.CtPullDownList.gameObject.SetActive(!active);
        }

        private void OnBtnResetClick()
        {
        }

        private void OnBtnAutoClick()
        {
        }

        private void OnBtnFinishClick()
        {
            PanelGame.Inst.EndPlayGame();
            this.ShowPullDownList();
        }

        private void OnBtnStatusClick()
        {
            PanelGame.Inst.ShowStatus();
            this.ShowPullDownList();
        }

        private void ShowStatus()
        {
        }

        private void OnBtnShopClick()
        {
        }

        private void OnBtnExchangeClick()
        {
            PanelExchange.Inst.Open(null);
            this.ShowPullDownList();
        }
    }
}
