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

        public ExchangeBrowseView BrowseView;
        public ExchangeDetailView DetailView;
        public ExchangeRemainNullView NullView;
        public ExchangeEnsureView EnsureView;
        public ExchangeResultView ResultView;

        [SerializableAttribute]
        public class ExchangeBrowseView : IPanelChildExchange
        {
            public GameObject Root;
            private ChildName _childName = ChildName.BrowseView;
            public ChildName ChildName
            {
                get
                {
                    return _childName;
                }
                set
                {
                    _childName = value;
                }
            }
        }

        [SerializableAttribute]
        public class ExchangeDetailView : IPanelChildExchange
        {
            public GameObject Root;
            public ChildName ChildName{get;set;}

            public UISprite Icon_1;
            public UISprite Icon_2;

            public UILabel Desc;

            public UIButton BtnChange;
            public UILabel RemainNum;
            public UIInput InputNum;

            public UILabel CostBallsNum;
            public UILabel RemainBallsNum;
        }

        [SerializableAttribute]
        public class ExchangeRemainNullView : IPanelChildExchange
        {
            public GameObject Root;
            public ChildName ChildName{get;set;}

            public UILabel Desc;
        }

        [SerializableAttribute]
        public class ExchangeEnsureView : IPanelChildExchange
        {
            public GameObject Root;
            public ChildName ChildName{get;set;}
            public UILabel Tips;

            public UISprite Icon_1;
            public UISprite Icon_2;

            public UILabel Desc;

            public UIButton BtnChange;
            public UILabel RemainNum;
            public UIInput InputNum;
        }

        [SerializableAttribute]
        public class ExchangeResultView : IPanelChildExchange
        {
            public GameObject Root;
            public ChildName ChildName{get;set;}
            public UILabel Desc;
            public UISprite Icon_1;
            public UISprite Icon_2;
            public UILabel Num;
        }

        public enum ChildName
        {
            BrowseView,
            DetailView,
            NullView,
            EnsureView,
            ResultView
        }

        public interface IPanelChildExchange : IPanelChild
        {
            ChildName ChildName{get;set;}
        }

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
            PanelManager.Open(this.gameObject);
            Player.Inst.State = PlayerStateType.Exchange;
            this.OpenChild(this.BrowseView);
        }

        public override void OpenChild(object data)
        {
            base.OpenChild(data);
            var child = data as IPanelChildExchange;
            switch(child.ChildName)
            {
                case ChildName.BrowseView:
                    this.OpenToBrowseView();
                    break;
                case ChildName.DetailView:
                    this.OpenToDetailView();
                    break;
                case ChildName.NullView:
                    this.OpenToNullView();
                    break;
                case ChildName.EnsureView:
                    this.OpenToEnsureView();
                    break;
                case ChildName.ResultView:
                    this.OpenToResultView();
                    break;
            }
        }

        private void Awake()
        {
            base.Awake();
            this.gameObject.SetActive(false);
        }

        private List<List<GoodsData>> GetTestDatas()
        {
            var datas = new List<List<GoodsData>>(5);
            int index = 0;
            for(int i = 0; i < 5; i++)
            {
                //temp.Clear();
                var temp = new List<GoodsData>(2);
                for(int j = 0; j < 2; j++)
                {
                    GoodsData data = new GoodsData();
                    data.GoodsName = string.Format("Goods_{0}", index); 
                    Debug.Log(data.GoodsName);
                    data.RemainNum = 10;
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
#region BrowseView
        public void OpenToBrowseView()
        {
            this.Init();
            PanelManager.OpenChild(this.BrowseView.Root);
        }
#endregion
#region DetailView
        public void OpenToDetailView()
        {
            this.InitDetailView();            
            PanelManager.Open(this.DetailView.Root);
        }

        private void InitDetailView()
        {
        }

#endregion

#region NullView;
        public void OpenToNullView()
        {
            this.InitNullView();
            PanelManager.Open(this.NullView.Root);
        }

        private void InitNullView()
        {
        }
#endregion

#region EnsureView;
        public void OpenToEnsureView()
        {
            this.InitEnsureView();
            PanelManager.Open(this.EnsureView.Root);
        }

        private void InitEnsureView()
        {
        }
#endregion

#region ResultView;
        public void OpenToResultView()
        {
            this.InitResultView();
            PanelManager.Open(this.ResultView.Root);
        }

        private void InitResultView()
        {
        }
#endregion

    }
}
