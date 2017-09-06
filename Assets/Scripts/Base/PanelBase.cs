//================================
//===Author: easy
//===Email: gopiny@live.com
//===Date: 2017-09-06 09:25
//================================

using System;
using UnityEngine;

namespace Asobimo.Pachinko
{
    public interface IPanel
    {
        void Open();
    }

    public abstract class PanelBase<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Inst;
        private object _objSync = new object();

        protected void Awake()
        {
            lock(_objSync)
            {
                if(Inst == null)
                {
                    Inst = this as T;
                }
            }
        }

        public abstract void Open();
        public abstract void Close();
        public abstract void Back();

        public virtual void Home()
        {

        }

        protected void Destroy()
        {
            Debug.Log("Panel Destory");
            Inst = null;
        }
    }
}

