//================================
//===Author: easy
//===Email: gopiny@live.com
//===Date: 2017-09-08 09:59
//================================

using UnityEngine;
using CustomControl;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Asobimo.Pachinko
{
    public class ScrollItemMain : ScrollViewItem
    {
        private ItemPachinko[] _items;
        private List<PachinkoData> _datas;
        public override void FillItem(IList datas, int index)
        {
            base.FillItem(datas, index);
            _datas = (List<PachinkoData>)datas[index];
            if(_items == null)
            {
                _items = gameObject.GetComponentsInChildren<ItemPachinko>();
            }

            if (index == 2)
                Debug.Log("Debug");

            for(int i = 0; i < _items.Length; i++)
            {
                if(i > _datas.Count - 1)
                {
                    _items[i].gameObject.SetActive(false);
                    continue;
                }
                _items[i].FillItem(_datas[i]);
            }
        }
    }
}
