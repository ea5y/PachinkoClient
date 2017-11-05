using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Easy.FrameUnity.Util;

namespace Easy.FrameUnity.Net
{
    public static class ActionID
    {
        public static readonly int Register = 1001;
        public static readonly int Login = 1002;
        //public static readonly int GetUserData = 1003;
        public static readonly int GetPachinkoData = 1004;
        public static readonly int GetGoods = 1005;
        public static readonly int Switch = 1006;
        public static readonly int ChangeLevel = 1007;
    }

    public class CastID
    {
        public const int ChangePachinkoData = 1001;
        public const int ChangePachinkoInput = 1002;
    }

    public class Net : MonoBehaviour
    {
        private static Queue<Action> _dispatchQueue = new Queue<Action>();
        
        private static object _objAsync = new object();

        private void OnDestroy()
        {
            SocketClient.CloseConnection();
            Debug.Log("Quit");
        }

        private void Update()
        {
            if(_dispatchQueue.Count > 0)
            {
                Queue<Action> actionQueue = new Queue<Action>();
                Action action;
                lock(_objAsync)
                {
                    while(_dispatchQueue.Count > 0)
                    {
                        action = _dispatchQueue.Dequeue();
                        actionQueue.Enqueue(action);
                    }
                }
                while(actionQueue.Count > 0)
                {
                    action = actionQueue.Dequeue();
                    action.Invoke();
                }
            }
        }

        public static void InvokeAsyncForCS(Action action)
        {
            lock(_objAsync)
            {
                _dispatchQueue.Enqueue(()=>{
                        action();
                        });
            }
        }

        public static void InvokeAsyncForLUA(Action action)
        {

        }

#region BroadCast
        public static Dictionary<int, Action<byte[]>> CastDic = new Dictionary<int, Action<byte[]>>()
        {
            {CastID.ChangePachinkoData, (res)=>{}},
            {CastID.ChangePachinkoInput, (res)=>{}},
        };

        private static void ChangePachinkoData(byte[] res)
        {
            var obj = ProtoBufUtil.Deserialize<PachinkoDataSet>(res);
            //@TODO
        }

        private static void ChangePachinkoInput(byte[] res)
        {
            var obj = ProtoBufUtil.Deserialize<PachinkoInputData>(res);
            //@TODO
        }
#endregion

#region Request
        public static void Login(string username, string password, Action<LoginDataRes> callback)
        {
            var data = new RegisterData() { Username = username, Password = password };
            var bytes = PackageFactory.Pack(ActionID.Login, data, callback);
            SocketClient.Send(bytes);
        }
#endregion
    }
}
