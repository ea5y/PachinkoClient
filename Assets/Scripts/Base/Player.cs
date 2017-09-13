//================================
//===Author: easy
//===Email: gopiny@live.com
//===Date: 2017-09-11 10:13
//================================

using System;
using UnityEngine;
namespace Asobimo.Pachinko
{
    public enum PlayerStateType
    {
        None,
        Browsing,
        Watching,
        Playing
    }

    public class PlayerStateArgs : EventArgs
    {
        public PlayerStateType State;
        public PlayerStateArgs(PlayerStateType state)
        {
            this.State = state;
        }
    }

    public class PlayerBallsNumArgs : EventArgs
    {
        public int BallsNum;
        public PlayerBallsNumArgs(int num)
        {
            this.BallsNum = num;
        }
    }

    public class Player  
    {
        //public event EventHandler OnStateChange = (s, e)=>{};
        public event EventHandler<PlayerStateArgs> OnStateChanged = (s, e)=>{};
        private PlayerStateType _state = PlayerStateType.None;
        public PlayerStateType State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
                this.OnStateChanged(this, new PlayerStateArgs(_state));
            }
        }

        public event EventHandler<PlayerBallsNumArgs> OnBallsNumChanged = (s, e)=>{};
        private int _ballsNum = 0;
        public int BallsNum
        {
            get
            {
                return _ballsNum;
            }
            set
            {
                _ballsNum = value;
                this.OnBallsNumChanged(this, new PlayerBallsNumArgs(_ballsNum));
            }
        }

        private static Player _inst;
        public static Player Inst
        {
            get
            {
                if(_inst == null)
                    _inst = new Player();
                return _inst;
            }
        }

        public void Init()
        {
            this.OnStateChanged(this, new PlayerStateArgs(_state));
            this.OnBallsNumChanged(this, new PlayerBallsNumArgs(_ballsNum));
        }
    }
}
