using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackState : StateMachineBehaviour
{
    UnitManager unitManager;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        unitManager = animator.GetComponentInParent<UnitManager>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject nearestEnemy = GameObject.FindWithTag(unitManager.enemyTeam[(int)unitManager.returnTeamAffliation]);
        unitManager.agent.SetDestination(nearestEnemy.transform.position);

        if (nearestEnemy == null)
        {
            Debug.LogError("ERROR: No enemies detected");
            return;
        }

        float distance = Vector3.Distance(unitManager.agent.transform.position, nearestEnemy.transform.position);
        if (distance >= unitManager.range)
        {
            unitManager.agent.speed = unitManager.speed;
            animator.SetTrigger("Attack");
        }
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
