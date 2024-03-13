using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackState : StateMachineBehaviour
{
    UnitManager unit;
    GameObject hitbox;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        unit = animator.GetComponentInParent<UnitManager>();
        hitbox = unit.hitbox;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (unit.health <= 0.0f)
        {
            animator.SetTrigger("Dead");
            return;
        }
        
        GameObject nearestEnemy = GameObject.FindWithTag(unit.enemyTeam[unit.returnTeamAffliation]);
        unit.agent.SetDestination(nearestEnemy.transform.position);

        if (nearestEnemy == null)
        {
            Debug.LogError("ERROR: No enemies detected");
            return;
        }

        unit.transform.LookAt(nearestEnemy.transform);

        float distance = Vector3.Distance(unit.agent.transform.position, nearestEnemy.transform.position);
        if (distance >= unit.range)
        {
            unit.agent.speed = unit.speed;
            animator.SetTrigger("Walk");
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
