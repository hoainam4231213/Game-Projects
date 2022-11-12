using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_System : MonoBehaviour
{
    #region FSM
    private FSM_State curentState_;
    public FSM_State CurrentState
    {
        get
        {
            return curentState_;
        }
    }
    public void GotoState(FSM_State new_state)
    {
        curentState_?.Exits();
        curentState_ = new_state;
        new_state.OnEnter();
    }
    public void GotoState(FSM_State new_state,object data)
    {
        curentState_?.Exits();
        curentState_ = new_state;
        new_state.OnEnter(data);
    }

    #endregion

    #region Messagge from Unity
    public virtual void Awake()
    {

    }
    // Start is called before the first frame update
    public  virtual void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        curentState_?.Update();
    }

 

    public virtual void FixedUpdate()
    {
        curentState_?.FixedUpdate();
    }

    public virtual void LateUpdate()
    {
        curentState_?.LateUpdate();
    }
    #endregion

}
