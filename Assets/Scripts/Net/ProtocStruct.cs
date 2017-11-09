//================================
//===Author: easy
//===Email: gopiny@live.com
//===Date: 2017-11-05 20:04
//================================

using Asobimo.Pachinko;
using System.Collections.Generic;
namespace Easy.FrameUnity.ESNetwork
{
    #region Req && Res

    #region Register && Login
    public class RegisterDataReq
	{
		public string Username;
		public string Password;
        public string Sid;
	}

	public class RegisterDataRes
	{
        public int ReturnCode;
	}

	public class LoginDataReq
	{
	}

	public class LoginDataRes
	{
        public int ReturnCode;
		public string SessionId;
		public UserData UserData;
	}

	public class UserData
	{
		public int UserId;
		public string NickName;
		public int BallsNum;
	}
    #endregion

    #region GetPachinko
    public class GetPachinkosRes
    {
        public int ReturnCode;
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
        public PachinkoType Type;
    }
    #endregion

    #region DealSwitch
    public class DealSwitchReq
    {
        public int PachinkoId;
        public string SwitchType;
    }

    public class DealSwitchRes
    {
        public int ReturnCode;
    }
    #endregion

    #endregion

    #region Cast
    public class PachinkoStateDataCast
    {
        public int Id;
        public PachinkoStateType StateType;
    }
    #endregion
}
