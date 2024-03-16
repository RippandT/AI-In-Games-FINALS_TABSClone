using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericIdleState : StateMachineBehaviour
{
    UnitManager unit;
    float startingHealth;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        unit = animator.GetComponentInParent<UnitManager>();
        unit.agent.ResetPath();
        unit.GetComponent<Rigidbody>().velocity = Vector3.zero;
        unit.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (unit.health <= 0.0f)
        {
            animator.SetTrigger("Dead");
            return;
        }

        GameObject nearestEnemy = GameObject.FindWithTag(unit.enemyTeam[unit.returnTeamAffliation]);

        // Immediately go to Walk state when threatened
        if (unit.health < startingHealth || nearestEnemy != null)
        {
            GoIntoAction(animator);
        }
    }

    void GoIntoAction(Animator animator)
    {
        animator.SetBool("IsWalking", true);
        animator.SetBool("IsIdle", false);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
