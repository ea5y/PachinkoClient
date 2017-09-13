//================================
//===Author: easy
//===Email: gopiny@live.com
//===Date: 2017-09-06 12:49
//================================

using UnityEngine;
namespace Asobimo.Pachinko
{
    public enum PachinkoStateType
    {
        Unoccupied,
        Occupied,
        Owned,
        Maintain,
        Reset,
        LostConnection
    }
    
    public abstract class State
    {
        public abstract void Enter(object data);
        public abstract void Start();
        public abstract void End();
        public virtual void Maintain()
        {
            Debug.Log("===>Maintain");
        }

        public virtual void Exit()
        {
            Debug.Log("===>Exit");
            Player.Inst.State = PlayerStateType.None;
        }
    }

    //The pachinko is unoccupied
    public class UnoccupiedState : State
    {
        private Pachinko _pachinko;
        public UnoccupiedState(Pachinko pachinko)
        {
            _pachinko = pachinko;
        }

        public override void Enter(object data)
        {
            Debug.Log("===>State(Unoccupied) Enter");
            PanelGame.Inst.Open(data);
            //Player.Inst.State = PlayerStateType.Browsing;
        }

        public override void Start()
        {
            Debug.Log("====>State(Unoccupied) Start");
            PanelGame.Inst.SetCurPacinkoState(PachinkoStateType.Owned);
        }

        public override void End()
        {
            Debug.LogError("===>State(Unoccupied) Pachinko is be Unoccupied");
        }
    }

    //The pachinko is occupied 
    public class OccupiedState : State
    {
        private Pachinko _pachinko;
        public OccupiedState(Pachinko pachinko)
        {
            _pachinko = pachinko;
        }

        public override void Enter(object data)
        {
            Debug.Log("===>State(Occupied) Enter");
            PanelGame.Inst.Open(data);
        }

        public override void Start()
        {
            Debug.LogError("===>State(Occupied) Pachinko is be Occupied");
        }

        public override void End()
        {
            Debug.LogError("===>State(Occupied) Pachinko is be Occupied");
        }
    }

    //The machine is yours
    public class OwnedState : State
    {
        private Pachinko _pachinko;
        public OwnedState(Pachinko pachinko)
        {
            _pachinko = pachinko;
        }

        public override void Enter(object data)
        {
            Debug.LogError("===>State(Owned) Pachinko is Owned");
        }

        public override void Start()
        {
            Debug.LogError("===>State(Owned) Pachinko is Owned");
        }

        public override void End()
        {
            Debug.Log("===>State(Owned) End");
            PanelGame.Inst.SetCurPacinkoState(PachinkoStateType.Unoccupied);
            PanelResult.Inst.Open(null);
        }
    }

    //The machine is maintainning
    public class MaintainState : State
    {
        private Pachinko _pachinko;
        public MaintainState(Pachinko pachinko)
        {
            _pachinko = pachinko;
        }

        public override void Enter(object data)
        {
            Debug.LogError("===>State(Maintain) Pachinko is Maintaining");
        }

        public override void Start()
        {
            Debug.LogError("===>State(Maintain) Pachinko is Maintaining");
        }

        public override void End()
        {
            Debug.LogError("===>State(Maintain) Pachinko is Maintaining");
        }
    }

    //When player who occupys the pachinko pauses game
    //Display the remain time, when the remain time equal 0 the player will be force breaked with current pachinko
    //Remain time is 10m
    public class ResetState : State
    {
        private Pachinko _pachinko;
        public ResetState(Pachinko pachinko)
        {
            _pachinko = pachinko;
        }

        public override void Enter(object data)
        {
            Debug.Log("===>State(Reset) Enter");
            Player.Inst.State = PlayerStateType.Browsing;
        }

        public override void Start()
        {
            Debug.LogError("===>State(Reset) Pachinko is Resetting");
        }

        public override void End()
        {
            Debug.LogError("===>State(Reset) Pachinko is Resetting");
        }
    }

    //When player lost connection 
    //Display the remain time, when the remain time equal 0 the player will be force breaked with current pachinko
    //Remain time is 5m 
    public class LostConnectionState : State
    {
        private Pachinko _pachinko;
        public LostConnectionState(Pachinko pachinko)
        {
            _pachinko = pachinko;
        }

        public override void Enter(object data)
        {
            Debug.Log("===>State(LostConnection) Enter");
            Player.Inst.State = PlayerStateType.Browsing;
        }

        public override void Start()
        {
            Debug.LogError("===>State(LostConnection) Pachinko is LostConnectioning");
        }

        public override void End()
        {
            Debug.LogError("===>State(LostConnection) Pachinko is LostConnectioning");
        }
    }

    public class Pachinko
    {
        public readonly State UnoccupiedState;
        public readonly State OccupiedState;
        public readonly State OwnedState;
        public readonly State MaintainState;
        public readonly State ResetState;
        public readonly State LostConnectionState;

        private State _state;
        public State State
        {
            get{return _state;}
            set{_state = value;}
        }

        public Pachinko()
        {
            this.UnoccupiedState = new UnoccupiedState(this);
            this.OccupiedState = new OccupiedState(this);
            this.OwnedState = new OwnedState(this);
            this.MaintainState = new MaintainState(this);
            this.ResetState = new ResetState(this);
            this.LostConnectionState = new LostConnectionState(this);
            this.State = UnoccupiedState;
        }

        public void Enter(object data)
        {
            _state.Enter(data);
        }

        public void Start()
        {
            _state.Start();
        }

        public void End()
        {
            _state.End();
        }

        public void Exit()
        {
            _state.Exit();
        }

        public void Maintain()
        {
            _state.Maintain();
        }

        public void SetState(State state)
        {
            _state = state;
        }
    }
}
