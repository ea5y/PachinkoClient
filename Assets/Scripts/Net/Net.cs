//================================
//===Author: easy
//===Email: gopiny@live.com
//===Date: 2017-11-05 10:55
//================================

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using UnityEngine;
using LitJson;
using Easy.FrameUnity.ESThread;
using Easy.FrameUnity.Util;

namespace Easy.FrameUnity.ESNetwork
{
	public class NetPackage
	{
		public string MsgId;
		public string ProtocId;
		public static string Sid;
		public string Uid;
		public string Type;
		public string Version;
		public string Data;
	}

	public class Net : MonoBehaviour
	{
		public enum ActionID
		{
			Heartbeat = 1,
			Register = 1001,
			Login = 1002,
            DealSwitch = 1003,
            DealLevel = 1004,
            GetPachinkos = 1005,
            GetGoods = 1006
		}

		public enum BrodcastID
		{
			ChangePachinkoData = 1001,
			ChangePachinkoInput = 1002,
		}

		private static Queue<NetPackage> _recvResponseQueueCS = new Queue<NetPackage>();
		private static Queue<NetPackage> _recvResponseQueueLUA = new Queue<NetPackage>();
		private static Queue<NetPackage> _recvBrodcastQueueCS = new Queue<NetPackage>();
		private static Queue<NetPackage> _recvBrodcastQueueLUA = new Queue<NetPackage>();
		private static object _syncResQueueCS = new object();
		private static object _syncResQueueLUA = new object();
		private static object _syncBrodQueueCS = new object();
		private static object _syncBrodQueueLUA = new object();

		private static Dictionary<string, Action<string>> _responseCallbacksCS = new Dictionary<string, Action<string>>();
		private static Dictionary<string, Action<string>> _responseCallbacksLUA = new Dictionary<string, Action<string>>();
		private static Dictionary<string, Action<string>> _brodcastActionCS = new Dictionary<string, Action<string>>();
		private static Dictionary<string, Action<string>> _brodcastActionLUA = new Dictionary<string, Action<string>>();

		private static int _msgId = 1;
		public static int MsgId
		{
			get{return ++_msgId;}
		}

		public static void TransformBytes(byte[] bytes)
		{
			int statusCode;
			var package = Unpack(bytes, out statusCode);
            if (GetError(statusCode))
                return;

            if (package.Type == "response")
            {
                DealResponse(package);
            }
            else
            {
                DealBrodcast(package);
            }
		}

        private static bool GetError(int code)
        {
            var msg = string.Format("RequestStatus: {0}", ErrorCode.Dic[code]);
            MainThread.Log(msg);
            if (code == 0)
                return false;
            else
            {
                return true;
            }
        }

		private static void DealResponse(NetPackage package)
		{
			if(package.Version == "origin")
			{
				EnqueueResponseQueueCS(package);
			}
			else
			{
				EnqueueResponseQueueLUA(package);
			}
		}

		private static void DealBrodcast(NetPackage package)
		{
			if(package.Version == "origin")
			{
				EnqueueBrodcastQueueCS(package);
			}
			else
			{
				EnqueueBrodcastQueueLUA(package);
			}
		}

		public static byte[] Pack(NetPackage package)
		{
			var packageStr = string.Format("MsgId={0}&ActionId={1}&Sid={2}&Uid={3}&Data={4}",
					package.MsgId, package.ProtocId, NetPackage.Sid, package.Uid, package.Data);
			Debug.Log("Send:" + packageStr);
			byte[] tempBytes = Encoding.ASCII.GetBytes(packageStr);
			byte[] len = BitConverter.GetBytes(tempBytes.Length);
			byte[] resultBytes = new byte[tempBytes.Length + len.Length];
			Buffer.BlockCopy(len, 0, resultBytes, 0, len.Length);
			Buffer.BlockCopy(tempBytes, 0, resultBytes, len.Length, tempBytes.Length);
			return resultBytes;
		}

		private static NetPackage Unpack(byte[] bytes, out int statusCode)
		{
			int pos = 0;
			int bytesLen = GetInt(bytes, ref pos);
			if(bytesLen != bytes.Length)
				throw new Exception("Package is not full!");

			statusCode = GetInt(bytes, ref pos);

			var package = new NetPackage();
			package.MsgId = GetInt(bytes, ref pos).ToString();
			var Description = GetString(bytes, ref pos);
			package.ProtocId = GetInt(bytes, ref pos).ToString();
			var StrTime = GetString(bytes, ref pos);
			package.Type = GetString(bytes, ref pos);
			package.Version = GetString(bytes, ref pos);
			package.Data = GetString(bytes, ref pos);

			return package;
		}

		private static int GetInt(byte[] bytes, ref int pos)
		{
			int val = BitConverter.ToInt32(bytes, pos);
			pos += sizeof(int);
			return val;
		}

		private static string GetString(byte[] bytes, ref int pos)
		{
			string val = string.Empty;
			int len = GetInt(bytes, ref pos);
            if (len > 0)
            {
                val = Encoding.UTF8.GetString(bytes, pos, len);
                pos += len;
            }
            return val;
		}

		public static void EnqueueResponseQueueCS(NetPackage package)
		{
			lock(_syncResQueueCS)
			{
				_recvResponseQueueCS.Enqueue(package);
			}
		}

		public static void EnqueueResponseQueueLUA(NetPackage package)
		{
			lock(_syncResQueueLUA)
			{
				_recvResponseQueueLUA.Enqueue(package);
			}
		}

		public static void EnqueueBrodcastQueueCS(NetPackage package)
		{
			lock(_syncBrodQueueCS)
			{
				_recvBrodcastQueueCS.Enqueue(package);
			}
		}

		public static void EnqueueBrodcastQueueLUA(NetPackage package)
		{
			lock(_syncBrodQueueLUA)
			{
				_recvBrodcastQueueLUA.Enqueue(package);
			}
		}

		private Queue<NetPackage> GetDispatchQueue(Queue<NetPackage> queue, object syncObj)
		{
			Queue<NetPackage> dispatchQueue = new Queue<NetPackage>();
			lock(syncObj)
			{
				while(queue.Count > 0)
				{
					dispatchQueue.Enqueue(queue.Dequeue());
				}
			}
			return dispatchQueue;
		}

		private void Dispatch(Queue<NetPackage> dispatchQueue, Dictionary<string, Action<string>> actions)
		{
			while(dispatchQueue.Count > 0)
			{
				var package = dispatchQueue.Dequeue();
				Action<string> action;
				if(actions.TryGetValue(package.MsgId, out action))
				{
					action.Invoke(package.Data);
					actions.Remove(package.MsgId);
				}
			}
		}

		private void DispatchResponseCS()
		{
			if(_recvResponseQueueCS.Count > 0)
			{
				var dispatchQueue = GetDispatchQueue(_recvResponseQueueCS, _syncResQueueCS);
				Dispatch(dispatchQueue, _responseCallbacksCS);
			}
		}

		private void DispatchResponseLUA()
		{
			if(_recvResponseQueueLUA.Count > 0)
			{
				var dispatchQueue = GetDispatchQueue(_recvResponseQueueLUA, _syncResQueueLUA);
				Dispatch(dispatchQueue, _responseCallbacksLUA);
			}
		}

		private void DispatchBrodcastCS()
		{
			if(_recvBrodcastQueueCS.Count > 0)
			{
                Debug.LogError("Debug");
				var dispatchQueue = GetDispatchQueue(_recvBrodcastQueueCS, _syncBrodQueueCS);
				Dispatch(dispatchQueue, _brodcastActionCS);
			}
		}

		private void DispatchBrodcastLUA()
		{
			if(_recvBrodcastQueueLUA.Count > 0)
			{
				var dispatchQueue = GetDispatchQueue(_recvBrodcastQueueLUA, _syncBrodQueueLUA);
				Dispatch(dispatchQueue, _brodcastActionLUA);
			}
		}

		private static void AddActionToResponseCallbacksCS(string msgId, Action<string> action)
		{
			_responseCallbacksCS.Add(msgId, action);
		}

        public static void AddActionToResponseCallbacksLUA(string msgId, Action<string> action)
        {
            _responseCallbacksLUA.Add(msgId, action);
        }

		private void Update()
		{
			DispatchResponseCS();
			DispatchResponseLUA();
			DispatchBrodcastCS();
			DispatchBrodcastLUA();
		}

		private void OnDestroy()
		{
            if (_heartbeatTimer != null)
                _heartbeatTimer.Dispose();
			SocketClient.CloseConnection();
            Debug.Log("Dipose Socket!");
		}

        #region Request

        private static void Send<T>(object data, int protocId, Action<T> callback)
		{
			string dataJson = string.Empty;
			if(data != null)
				dataJson = JsonMapper.ToJson(data);
			var package = new NetPackage();
			package.MsgId = MsgId.ToString();
			package.ProtocId = protocId.ToString();
			package.Data = dataJson;
			var bytes = Pack(package);

			if(callback != null)
				AddActionToResponseCallbacksCS(package.MsgId, (res)=>{
						var obj = JsonMapper.ToObject<T>(res);
						callback(obj);
						});

			SocketClient.Send(bytes);
		}

		private static void Send(object data, int protocId, int msgId)
		{
			string dataJson = string.Empty;
			if(data != null)
				dataJson = JsonMapper.ToJson(data);
			var package = new NetPackage();
			package.MsgId = msgId.ToString();
			package.ProtocId = protocId.ToString();
			package.Data = dataJson;
			var bytes = Pack(package);

			SocketClient.Send(bytes);
		}

        private static Timer _heartbeatTimer;
		public static void Heartbeat()
		{
            if(_heartbeatTimer == null)
            {
                _heartbeatTimer = new Timer(SendHeartbeat, null, 10000, 10000);
            }
		}

        private static void SendHeartbeat(object state)
        {
            Send(null, (int)ActionID.Heartbeat, 1);
        }

		public static void Login(RegisterDataReq data, string password, Action<LoginDataRes> callback)
		{
			Send(data, (int)ActionID.Login, callback);
		}

        public static void GetPachinkos(Action<GetPachinkosRes> callback)
        {
            Send(null, (int)ActionID.GetPachinkos, callback);
        }

        public static void GetGoods()
        {

        }

        public static void StartPlayGame(int pachinkoId)
        {
            var data = new DealSwitchReq();
            data.PachinkoId = pachinkoId;
            data.SwitchType = "on";
            Action<DealSwitchRes> callback = (res) => { };
            Send(data, (int)ActionID.DealSwitch, callback);
        }

        public static void EndPlayGame(int pachinkoId)
        {
            var data = new DealSwitchReq();
            data.PachinkoId = pachinkoId;
            data.SwitchType = "off";
            Action<DealSwitchRes> callback = (res) => { };
            Send(data, (int)ActionID.DealSwitch, callback);
        }

        public static void ChangeLevel(int pachinkoId, string level)
        {

        }
        #endregion

        #region Bordcast
        private void OnChangePachinkoState(string res)
        {
            var data = JsonMapper.ToObject<PachinkoStateDataCast>(res);
            Asobimo.Pachinko.PanelMain.Inst.ChangePachinkoState(data);
            //Find pachinko
            //Change pachinko data
            //Change pachinko item
        }
        #endregion
    }
}
