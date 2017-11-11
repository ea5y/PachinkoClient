using UnityEngine;
using System.Collections.Generic;
using System;
using CustomControl;
using Easy.FrameUnity.ESNetwork;
using LitJson;

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

		private List<List<PachinkoData>> _datasAll;
		private List<List<PachinkoData>> _datasRecommend;
        private void OnGetPachinkos(GetPachinkosRes res)
        {
            List<Easy.FrameUnity.ESNetwork.PachinkoData> all;
            List<Easy.FrameUnity.ESNetwork.PachinkoData> recommend;
            this.SplitPachinkos(res.PachinkoDataSet.PachinkoDataSetList, out all, out recommend);

            _datasAll = this.Pack(all);
            _datasRecommend = this.Pack(recommend);
            this.CreateScrollAll(_datasAll);
            this.CreateScrollRecommend(_datasRecommend);
        }

        private void SplitPachinkos(List<Easy.FrameUnity.ESNetwork.PachinkoData> dataList, 
            out List<Easy.FrameUnity.ESNetwork.PachinkoData> all, out List<Easy.FrameUnity.ESNetwork.PachinkoData> recommend)
        {
            all = new List<Easy.FrameUnity.ESNetwork.PachinkoData>();
            recommend = new List<Easy.FrameUnity.ESNetwork.PachinkoData>();

            foreach(var p in dataList)
            {
                if(p.Type == PachinkoType.Recommend)
                {
                    recommend.Add(p);
                }
                else
                {
                    all.Add(p);
                }
            }
        }

        private List<List<PachinkoData>> Pack(List<Easy.FrameUnity.ESNetwork.PachinkoData> dataList)
        {
            var datas = new List<List<PachinkoData>>();
            var dataListTemp = new List<Easy.FrameUnity.ESNetwork.PachinkoData>(dataList);
            var line = Math.Ceiling((float)dataList.Count / 2);
            Debug.Log("Line: " + line);
            int index = 0;
            for(int i = 0; i < line; i++)
            {
                var temp = new List<PachinkoData>();
                for(int j = 0; j < 2; j++)
                {
                    if (index > dataList.Count - 1)
                        continue;
                    var resData = dataListTemp[0];
                    var data = new PachinkoData();
                    data.Id = resData.Id;
                    data.index = index;
                    data.StateType = resData.StateType;
                    data.Times = resData.Times;
                    data.Sum = resData.Sum;
                    data.PbChange = resData.PbChange;
                    data.Award = resData.Award;
                    data.Type = resData.Type;
                    dataListTemp.RemoveAt(0);
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

        private void CreateScrollRecommend(List<List<PachinkoData>> datas)
        {
            if(_scrollViewRecommend == null)
            {
                _scrollViewRecommend = new ScrollView<ScrollViewItem>(TabPageRecommend.Grid, TabPageRecommend.ItemPrefab);
            }
            _scrollViewRecommend.CreateWeight(datas);
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
                    //this.SetPageRecommend();
                    _pages.SwitchTo(this.TabPageRecommend, this.BtnRecommend);
                    break;
            }
        }

        private void SetPageAll()
        {
        }

        private void SetPageRecommend()
        {
        }

        public void ChangePachinkoState(PachinkoStateDataCast data)
        {
			List<List<PachinkoData>> pDatas = null;
            if(data.Type == PachinkoType.Recommend)
            {
				pDatas = _datasRecommend;
            }
            else
            {
				pDatas = _datasAll;
            }

			if (pDatas == null)
				return;

			int index;
			if(this.GetPachinkoIndexById(pDatas, data.Id, out index))
			{
				_scrollViewRecommend.FindCellItemAndChange<PachinkoData, ItemPachinko>(index, 
						(pData, item)=>{
						pData.StateType = data.StateType;
						if(item != null)
						item.SetState(data.StateType);
						});
			}
        }

		private bool GetPachinkoIndexById(List<List<PachinkoData>> datas, int id, out int index)
		{
			index = -1;
			foreach(var list in datas)
			{
				foreach(var data in list)
				{
					if(data.Id == id)
					{
						index = data.index;
						return true;
					}
				}
			}
			return false;
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
