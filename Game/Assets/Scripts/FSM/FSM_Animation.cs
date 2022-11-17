using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_Animation : StateMachineBehaviour
{
    private FSM_System system;
    public float timeMiddle;
    private float timeCount;
    private bool isSendMiddle;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (system == null)
            system = animator.GetComponent<FSM_System>();
        system.BroadcastMessage("OnAnimationEnter", SendMessageOptions.DontRequireReceiver);
        timeCount = 0;
        isSendMiddle = false;
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        system.BroadcastMessage("OnAnimationUpdate", SendMessageOptions.DontRequireReceiver);
        timeCount += Time.deltaTime;
        if(timeCount >= timeMiddle && !isSendMiddle)
        {
            isSendMiddle = true;
            system.BroadcastMessage("OnAnimationMiddle", SendMessageOptions.DontRequireReceiver);
        }
        
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        system.BroadcastMessage("OnAnimationExit", SendMessageOptions.DontRequireReceiver);
    }
}
