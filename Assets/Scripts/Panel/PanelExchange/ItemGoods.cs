//================================
//===Author: easy
//===Email: gopiny@live.com
//===Date: 2017-09-15 18:28
//================================

using UnityEngine;
using CustomControl;

namespace Asobimo.Pachinko
{
    public class GoodsData : ScrollViewCellItemData
    {
        public string IconName;
        public string GoodsName;
        public int BallsNum;
        public string Status;
    }

    public class ItemGoods : ScrollViewCellItem
    {
        public UISprite Icon;

        public UILabel Name;
        public UILabel BallsNum;
        public UILabel Status;

        public UIButton BtnEnter;

        private GoodsData _data;
        public void FillItem(object data)
        {
            _data  = data as GoodsData;

            this.SetIcon();
            this.SetName();
            this.SetBallsNum();
            this.SetStatus();
            this.RetistBtnEvent();
        }

        private void RetistBtnEvent()
        {
            this.BtnEnter.onClick.Clear();
            EventDelegate.Add(this.BtnEnter.onClick, ()=>{
                    this.OnBtnEnterClick();
                    });
        }

        private void OnBtnEnterClick()
        {
            Debug.Log("===>Enter Exchange Detail");
        }

        private void SetIcon()
        {
            //this.Icon.spriteName = _data.IconName;
        }

        private void SetName()
        {
            this.Name.text = _data.GoodsName;
        }

        private void SetBallsNum()
        {
            this.BallsNum.text = _data.BallsNum.ToString();
        }

        private void SetStatus()
        {
            this.Status.text = _data.Status;
        }
    }
}
