using UnityEngine;
using System.Collections.Generic;
using System;
using CustomControl;
using Easy.FrameUnity.ESNetwork;

namespace Asobimo.Pachinko
{
    public class PanelMain : PanelBase<PanelMain>
    {
        [SerializableAttribute]
        public class AllPage : TabPage
        {
            [SerializeField]
            private GameObject _root;
            public UIScrollView ScrollView;
            public UIGrid Grid;
            public GameObject ItemPrefab;

            public override GameObject Root
            {
                get
                {
                    return _root;
                }
            }
        }

        [SerializableAttribute]
        public class RecommendPage : TabPage
        {
            [SerializeField]
            private GameObject _root;
            public UIScrollView ScrollView;
            public UIGrid Grid;
            public GameObject ItemPrefab;

            public override GameObject Root
            {
                get
                {
                    return _root;
                }
            }
        }

        public UIButton BtnAll;
        public UIButton BtnRecommend;
        public AllPage TabPageAll;
        public RecommendPage TabPageRecommend;

        private TabPagesManager _pages = new TabPagesManager();

        private ScrollView<ScrollItemMain> _scrollViewAll;
        private ScrollView<ScrollViewItem> _scrollViewRecommend;


        private enum PageType
        {
            TabPageAll,
            TabPageRecommend
        }

        private void Awake()
        {
            base.Awake();
            this.Init();
            this.RegisterBtnEvent();
            PanelManager.Open(this.gameObject);
            Net.GetPachinkos(OnGetPachinkos);
        }

        private void OnGetPachinkos(GetPachinkosRes res)
        {
            var datas = this.PackAll(res.PachinkoDataSet);
            this.CreateScrollAll(datas);
        }

        private List<List<PachinkoData>> GetTestDatas(PachinkoStateType type)
        {
            var datas = new List<List<PachinkoData>>(5);
            int index = 0;
            for(int i = 0; i < 5; i++)
            {
                var temp = new List<PachinkoData>(2);
                for(int j = 0; j < 2; j++)
                {
                    PachinkoData data = new PachinkoData();
                    data.StateType = type;
                    data.index = index;
                    temp.Add(data);
                    index++;
                }
                datas.Add(temp);
            }
            return datas;
        }

        private List<List<PachinkoData>> PackAll(PachinkoDataSet dataSet)
        {
            var datas = new List<List<PachinkoData>>();
            var line = Math.Ceiling((float)dataSet.PachinkoDataSetList.Count / 2);
            Debug.Log("Line: " + line);
            int index = 0;
            for(int i = 0; i < line; i++)
            {
                var temp = new List<PachinkoData>();
                for(int j = 0; j < 2; j++)
                {
                    if (index > dataSet.PachinkoDataSetList.Count - 1)
                        continue;
                    var resData = dataSet.PachinkoDataSetList[0];
                    var data = new PachinkoData();
                    data.index = index;
                    data.StateType = resData.StateType;
                    data.Times = resData.Times;
                    data.Sum = resData.Sum;
                    data.PbChange = resData.PbChange;
                    data.Award = resData.Award;
                    temp.Add(data);
                    index++;
                }
                datas.Add(temp);
            }
            return datas;
        }

        private void CreateScrollAll(List<List<PachinkoData>> datas)
        {
            if(_scrollViewAll == null)
            {
                _scrollViewAll = new ScrollView<ScrollItemMain>(TabPageAll.Grid, TabPageAll.ItemPrefab);
            }
            _scrollViewAll.CreateWeight(datas);
        }

        private void CreateScrollRecommend()
        {
        }

        private void Init()
        {
            _pages.AddPage(TabPageAll);
            _pages.AddPage(TabPageRecommend);
            _pages.AddBtnTab(BtnAll);
            _pages.AddBtnTab(BtnRecommend);
            this.SwitchTo(PageType.TabPageAll);
        }

        private void RegisterBtnEvent()
        {
            EventDelegate.Add(this.BtnAll.onClick, ()=>{
                    this.SwitchTo(PageType.TabPageAll);
                    });
            EventDelegate.Add(this.BtnRecommend.onClick, ()=>{
                    this.SwitchTo(PageType.TabPageRecommend);
                    });
        }

        private void SwitchTo(PageType type)
        {
            switch(type)
            {
                case PageType.TabPageAll:
                    //this.SetPageAll();
                    _pages.SwitchTo(this.TabPageAll, this.BtnAll);
                    break;
                case PageType.TabPageRecommend:
                    this.SetPageRecommend();
                    _pages.SwitchTo(this.TabPageRecommend, this.BtnRecommend);
                    break;
            }
        }

        private void SetPageAll()
        {
            if(_scrollViewAll == null)
            {
                _scrollViewAll = new ScrollView<ScrollItemMain>(TabPageAll.Grid, TabPageAll.ItemPrefab);
            }

            var datas = this.GetTestDatas(PachinkoStateType.Unoccupied);
            _scrollViewAll.CreateWeight(datas);
        }

        private void SetPageRecommend()
        {
            if(_scrollViewRecommend == null)
            {
                _scrollViewRecommend = new ScrollView<ScrollViewItem>(TabPageRecommend.Grid, TabPageAll.ItemPrefab);
            }
            var datas = this.GetTestDatas(PachinkoStateType.Occupied);
            _scrollViewRecommend.CreateWeight(datas);
        }

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
    }
}
