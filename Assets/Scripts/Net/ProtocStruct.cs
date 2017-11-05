//================================
//===Author: easy
//===Email: gopiny@live.com
//===Date: 2017-11-05 20:04
//================================

using System;
namespace Easy.FrameUnity.ESNetwork
{
	public class RegisterDataReq
	{
		public string Username;
		public string Password;
	}

	public class RegisterDataRes
	{
	}

	public class LoginDataReq
	{
	}

	public class LoginDataRes
	{
		public string SessionId;
		public UserData UserData;
	}

	public class UserData
	{
		public int UserId;
		public string NickName;
		public int BallsNum;
	}
}
