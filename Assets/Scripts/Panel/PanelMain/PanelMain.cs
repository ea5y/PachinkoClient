using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using CustomControl;

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

        private TabPagesManagerTest _pages = new TabPagesManagerTest();

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
        }

        private void Init()
        {
            _pages.AddPage(TabPageAll);
            _pages.AddPage(TabPageRecommend);
            _pages.AddBtnTab(BtnAll);
            _pages.AddBtnTab(BtnRecommend);
        }

        private void RegisterBtnEvent()
        {
            EventDelegate.Add(this.BtnAll.onClick, ()=>{
                    this.SetPageAll();
                    this.SwitchTo(PageType.TabPageAll);
                    });
            EventDelegate.Add(this.BtnRecommend.onClick, ()=>{
                    this.SetPageRecommend();
                    this.SwitchTo(PageType.TabPageRecommend);
                    });
        }

        private void SwitchTo(PageType type)
        {
            switch(type)
            {
                case PageType.TabPageAll:
                    _pages.SwitchTo(this.TabPageAll, this.BtnAll);
                    break;
                case PageType.TabPageRecommend:
                    _pages.SwitchTo(this.TabPageRecommend, this.BtnRecommend);
                    break;
            }
        }

        private void SetPageAll()
        {
            //@TODO
        }

        private void SetPageRecommend()
        {
            //@TODO
        }

        public override void Back()
        {
            throw new NotImplementedException();
        }

        public override void Close()
        {
            throw new NotImplementedException();
        }

        public override void Open()
        {
            throw new NotImplementedException();
        }
    }
}
