//================================
//===Author: easy
//===Email: gopiny@live.com
//===Date: 2017-11-03 11:00
//================================

using System;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;
using Easy.FrameUnity.ESThread;
namespace Easy.FrameUnity.ESNetwork
{
	public class SocketClient
	{
		public static Socket Socket;

		private static void CheckConnection()
		{
			if(Socket == null || !Socket.Connected)
			{
				OpenConnection();
				CreateReceiveThread();
			}
		}

		private static bool OpenConnection()
		{
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
				Debug.Log("Socket connect: " + URL.GAME_SERVER_HOST + ":" + URL.GAME_SERVER_PORT);
                Socket.Connect(URL.GAME_SERVER_HOST, URL.GAME_SERVER_PORT);
				return true;
            }
            catch (SocketException se)
            {
                CloseConnection();
                var msg = string.Format("Error: {0}", se.Message);
				Debug.Log(msg);
                throw se;
            }
			return false;
		}

		private static void CreateReceiveThread()
		{
			var thread = new Thread(new ThreadStart(Receive));
			thread.Start();
		}

		private static void Receive()
		{
			while(Socket != null)
			{
				if(Socket.Poll(-1, SelectMode.SelectRead))
				{
                    try
                    {
                        var bytes = GetBytes();
                        Net.TransformBytes(bytes);
                    }
                    catch(Exception e)
                    {
                        MainThread.Log("Error: " + e.Message);
                        break;
                    }
				}
			}
		}

		private static byte[] GetBytes()
		{
			int packageLen = GetPackageLen();
			byte[] bytes = new byte[packageLen];
			NetRead(bytes, packageLen);
			return bytes;
		}

		private static int GetPackageLen()
		{
			byte[] prefix = new byte[4];
			NetRead(prefix, 4);
			return BitConverter.ToInt32(prefix, 0);
		}

        private static int NetRead(byte[] data, int length)
        {
            int startIndex = 0;
            int recnum = 0;
            try
            {
                do
                {
                    int rev = Socket.Receive(data, startIndex, length - recnum, SocketFlags.None);
                    recnum += rev;
                    startIndex += rev;
                } while (recnum != length);
            }
            catch(SocketException se)
            {
                MainThread.Log(string.Format("Recieve Error: {0}", se.Message));
                CloseConnection();
                throw;
            }
            return recnum;
        }

		public static void CloseConnection()
		{
            if (Socket == null)
                return;
            Socket.Close();
            Socket = null;
		}

        //5.send
        public static void Send(byte[] data)
        {
            try
            {
                CheckConnection();
            }
            catch (SocketException se)
            {
                var msg = se.Message;
                //Debug.Log(msg);
                return;
            }

            if (Socket == null)
                return;
            //Debug.Log("===>Send");
            Socket.Send(data);
        }
	}

}
