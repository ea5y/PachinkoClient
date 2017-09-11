//================================
//===Author: easy
//===Email: gopiny@live.com
//===Date: 2017-09-11 12:43
//================================

using System;
using UnityEngine;
namespace Asobimo.Pachinko
{
    public class PanelResult : PanelBase<PanelResult>
    {
        public UIButton BtnBack;

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
            PanelManager.Open(this.gameObject);
        }

        private void Awake()
        {
            base.Awake();
            this.RegisterBtnEvent();
            this.gameObject.SetActive(false);
        }

        private void RegisterBtnEvent()
        {
            EventDelegate.Add(this.BtnBack.onClick, ()=>{
                    this.Home();
                    });
        }
    }
}
