//================================
//===Author: easy
//===Email: gopiny@live.com
//===Date: 2017-09-06 10:15
//================================

using UnityEngine;
using System.Collections;
using CustomControl;
namespace Asobimo.Pachinko
{
    public class MachineData
    {
        public Pachinko  Pachinko;
        public PachinkoData Data;
    }

    public class PachinkoData : ScrollViewCellItemData
    {
        public int Id;
        public PachinkoStateType StateType;

        public string IconName;
        public int Times;
        public int Sum;
        public int PbChange;
        public int Award;
        public PachinkoType Type;
        public int OwnerUserId;
    }

    public class ItemPachinko : ScrollViewCellItem
    {
        private Pachinko _pachinko;

        public UISprite ImgStatus;
        public UILabel LblStatus;

        public UISprite Icon;

        public UILabel Times;
        public UILabel Sum;
        public UILabel PbChange;
        public UILabel Award;

        public UIButton BtnEnter;

        private PachinkoData _data;
        public void FillItem(object data)
        {
            _data = data as PachinkoData;
            _pachinko = new Pachinko();
            this.index = _data.index;

            if(_data.OwnerUserId == Player.Inst.UserData.UserId)
            {
                this.SetState(PachinkoStateType.Owned);
            }
            else
            {
                this.SetState(_data.StateType);
            }
            this.SetDisplay();
            this.SetBtnEvent();
            Debug.Log("ItemMain index: " + _data.index);
        }

        public void SetState(PachinkoStateType stateType)
        {
            switch(stateType)
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

        private void SetUnoccupied()
        {
            _pachinko.State = _pachinko.UnoccupiedState;
            this.ImgStatus.color = Color.black;
            this.LblStatus.text = "Unoccupied";
        }

        private void SetOccupied()
        {
            _pachinko.State = _pachinko.OccupiedState;
            this.ImgStatus.color = Color.red;
            this.LblStatus.text = "Occupied";
        }

        private void SetOwned()
        {
            _pachinko.State = _pachinko.OwnedState;
            this.ImgStatus.color = Color.blue;
            this.LblStatus.text = "Owned";
        }

        private void SetMaintain()
        {
            _pachinko.State = _pachinko.MaintainState;
            this.ImgStatus.color = Color.yellow;
            this.LblStatus.text = "Maintain";
        }

        private void SetReset()
        {
            _pachinko.State = _pachinko.ResetState;
            this.ImgStatus.color = Color.green;
            this.LblStatus.text = "Reset";
        }

        private void SetLostConnection()
        {
            _pachinko.State = _pachinko.LostConnectionState;
            this.ImgStatus.color = Color.gray;
            this.LblStatus.text = "LostConnection";
        }

        public void SetDisplay()
        {
            this.Icon.spriteName = _data.IconName;
            this.Times.text = _data.Times.ToString();
            this.Sum.text = _data.Sum.ToString();
            this.PbChange.text = _data.PbChange.ToString();
            this.Award.text = _data.Award.ToString();
        }

        private void SetBtnEvent()
        {
            this.BtnEnter.onClick.Clear();
            EventDelegate.Add(this.BtnEnter.onClick, ()=>{
                    var data = new MachineData() { Pachinko = _pachinko, Data = _data };
                    _pachinko.Enter(data);
                    });
        }
    }
}
