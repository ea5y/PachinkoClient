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
        void OpenChild(object data);
        void Close();
        void Back();
        void Home();
    }

    public interface ITween
    {
        void Forward();
        void Reverse();
    }

    public interface IPanelChild
    {
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
        public virtual void OpenChild(object data)
        {
            Debug.Log("===>Child");
        }

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

        public static void OpenChild(GameObject panelChild)
        {
            panelChild.SetActive(true);
            _stack.Push(panelChild);
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
                if(panel.name == "PanelMain(Clone)")
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
            var cur = _stack.Pop();
            cur.SetActive(false);

            var pre = _stack.Peek();
            pre.SetActive(true);

            var panel = cur.GetComponent<IPanel>();
            if(panel != null)
            {
                panel.Back();
            }
            else
            {
                if(pre.GetComponent<IPanel>() != null)
                    Back();
            }
        }
    }
}

