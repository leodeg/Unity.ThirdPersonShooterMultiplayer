using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateBool : StateMachineBehaviour
{
    public string targetBool;
    public bool status;
    public bool resetOnExit;

    override public void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool (targetBool, status);
    }

    override public void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (resetOnExit)
        {
            animator.SetBool (targetBool, !status);
        }
    }
}
