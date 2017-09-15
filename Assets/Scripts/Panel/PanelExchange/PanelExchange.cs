//================================
//===Author: easy
//===Email: gopiny@live.com
//===Date: 2017-09-13 16:55
//================================

using System;
using UnityEngine;
using CustomControl;
using System.Collections.Generic;

namespace Asobimo.Pachinko
{
    public class PanelExchange : PanelBase<PanelExchange>
    {
        public GameObject ItemPrefab;
        public UIGrid Grid;

        private ScrollView<ScrollItemExchange> _scrollView;
        public override void Back()
        {
            //throw new NotImplementedException();
            Player.Inst.State = PlayerStateType.None;
        }

        public override void Close()
        {
            throw new NotImplementedException();
        }

        public override void Open(object data)
        {
            this.Init();
            PanelManager.Open(this.gameObject);
            Player.Inst.State = PlayerStateType.Exchange;
        }

        private void Awake()
        {
            base.Awake();
            this.gameObject.SetActive(false);
        }

        private List<List<GoodsData>> GetTestDatas()
        {
            var datas = new List<List<GoodsData>>(5);
            var temp = new List<GoodsData>(2);
            int index = 0;
            for(int i = 0; i < 5; i++)
            {
                temp.Clear();
                for(int j = 0; j < 2; j++)
                {
                    GoodsData data = new GoodsData();
                    data.GoodsName = string.Format("Goods_{0}", index); 
                    data.Status = "Remain 10";
                    data.index = index;
                    temp.Add(data);
                    index++;
                }
                datas.Add(temp);
            }
            return datas;
        }

        private void Init()
        {
            if(_scrollView == null)
                _scrollView = new ScrollView<ScrollItemExchange>(this.Grid, this.ItemPrefab);

            //Get data
            var datas = this.GetTestDatas();
            _scrollView.CreateWeight(datas);
        }
    }
}
