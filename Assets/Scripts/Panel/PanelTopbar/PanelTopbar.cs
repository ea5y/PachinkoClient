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
        public UILabel LblBallsNum;

        public UIWidget CtPullDownList;
        public UIButton BtnRest;
        public UIButton BtnAuto;
        public UIButton BtnFinish;
        public UIButton BtnStatus;
        public UIButton BtnShop;

        public UIWidget CtStatus;

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
            throw new NotImplementedException();
        }

        private void Awake()
        {
            base.Awake();
            this.RegisterBtnEvent();
        }

        private void RegisterBtnEvent()
        {
            EventDelegate.Add(this.BtnMenu.onClick, ()=>{ this.OnBtnMenuClick(); });
            EventDelegate.Add(this.BtnRest.onClick, ()=>{ this.OnBtnResetClick(); });
            EventDelegate.Add(this.BtnAuto.onClick, ()=>{ this.OnBtnAutoClick(); });
            EventDelegate.Add(this.BtnFinish.onClick, ()=>{ this.OnBtnFinishClick(); });
            EventDelegate.Add(this.BtnStatus.onClick, ()=>{this.OnBtnStatusClick(); });
            EventDelegate.Add(this.BtnShop.onClick, ()=>{this.OnBtnShopClick(); });
        }

        private void OnBtnMenuClick()
        {
            this.ShowPullDownList();
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
            PanelResult.Inst.Open(null);
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
    }
}
