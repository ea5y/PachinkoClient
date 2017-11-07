//================================
//===Author: easy
//===Email: gopiny@live.com
//===Date: 2017-11-05 20:04
//================================

using Asobimo.Pachinko;
using System.Collections.Generic;
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

    public class GetPachinkosRes
    {
        public PachinkoDataSet PachinkoDataSet;
    }

    public class PachinkoDataSet
    {
        public List<PachinkoData> PachinkoDataSetList = new List<PachinkoData>();
    }

    public class PachinkoData
    {
        public int Id;
        public PachinkoStateType StateType;
        public int Times;
        public int Sum;
        public int PbChange;
        public int Award;
    }
}
