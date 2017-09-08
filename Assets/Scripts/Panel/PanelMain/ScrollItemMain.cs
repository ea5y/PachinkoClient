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

            for(int i = 0; i < _items.Length; i++)
            {
                _items[i].FillItem(_datas[i]);
            }
        }
    }
}
