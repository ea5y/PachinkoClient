//================================
//===Author: easy
//===Email: gopiny@live.com
//===Date: 2017-09-15 18:26
//================================

using UnityEngine;
using CustomControl;
using System.Collections;
using System.Collections.Generic;

namespace Asobimo.Pachinko
{
    public class ScrollItemExchange : ScrollViewItem
    {
        private ItemGoods[] _items;
        private List<GoodsData> _datas; 
        public override void FillItem(IList datas, int index)
        {
            base.FillItem(datas, index);
            _datas = (List<GoodsData>)datas[index];
            if(_items == null)
            {
                _items = gameObject.GetComponentsInChildren<ItemGoods>();
            }

            for(int i = 0; i < _items.Length; i++)
            {
                _items[i].FillItem(_datas[i]);
            }
        }
    }
}
