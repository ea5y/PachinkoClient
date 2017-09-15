//================================
//===Author: easy
//===Email: gopiny@live.com
//===Date: 2017-09-06 09:25
//================================

using System;
using UnityEngine;
using System.Collections.Generic;

namespace Asobimo.Pachinko
{
    public interface IPanel
    {
        void Open(object data);
        void Close();
        void Back();
        void Home();
    }

    public abstract class PanelBase<T> : MonoBehaviour, IPanel where T : MonoBehaviour
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

        public abstract void Open(object data);
        public abstract void Close();
        public abstract void Back();

        public virtual void Home()
        {
            PanelManager.Home();
            Player.Inst.State = PlayerStateType.None;
        }

        protected void Destroy()
        {
            Debug.Log("Panel Destory");
            Inst = null;
        }
    }

    public class PanelManager
    {
        private static Stack<GameObject> _stack = new Stack<GameObject>();
        public static void Open(GameObject panelGameObj)
        {
            HidePrev();
            panelGameObj.SetActive(true);
            _stack.Push(panelGameObj);
        }

        private static void HideAll()
        {
            foreach(var panel in _stack)
            {
                panel.SetActive(false);
            }
        }

        private static void HidePrev()
        {
            if(_stack.Count > 0)
            {
                var panel = _stack.Peek();
                panel.SetActive(false);
            }
        }

        public static void Home()
        {
            while(true)
            {
                var panel = _stack.Peek();
                if(panel.name == "PanelMain")
                {
                    panel.SetActive(true);
                    break;
                }
                else
                {
                    panel.SetActive(false);
                    _stack.Pop();
                }
            }
        }

        public static void Back()
        {
            var go = _stack.Pop();
            go.SetActive(false);
            _stack.Peek().SetActive(true);
            var panel = go.GetComponent<IPanel>();
            panel.Back();
        }
    }
}

