//================================
//===Author: easy
//===Email: gopiny@live.com
//===Date: 2017-11-05 11:27
//================================

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Easy.FrameUnity.ESThread
{
	public class MainThread : MonoBehaviour
	{
		private static Queue<Action> _actionQueue = new Queue<Action>();
		private static object _syncObj = new object();

		private void OnUpdate()
		{
			if(_actionQueue.Count > 0)
			{
				var actionQueue = new Queue<Action>();
				lock(_syncObj)
				{
					while(_actionQueue.Count > 0)
					{
						var action = _actionQueue.Dequeue();
						actionQueue.Enqueue(action);
					}
				}
				while(actionQueue.Count > 0)
				{
					actionQueue.Dequeue().Invoke();
				}
			}
		}

		public static void InvokeAsync(Action action)
		{
			lock(_syncObj)
			{
				_actionQueue.Enqueue(action);
			}
		}

        public static void Log(string msg)
        {
            InvokeAsync(()=>{
                Debug.Log(msg);
            });
        }

        public static void LogError(string msg)
        {
            InvokeAsync(()=>{
                Debug.LogError(msg);
            });
        }
	}
}
