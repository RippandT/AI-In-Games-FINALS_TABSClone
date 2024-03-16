using DesignPatterns.ObjectPool;
using UnityEngine;

public class GunnerAttackState : StateMachineBehaviour
{
    UnitManager unit;
    GunProjectile gun;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        unit = animator.GetComponentInParent<UnitManager>();
        gun = animator.GetComponent<GunProjectile>();
        unit.agent.ResetPath();
        unit.agent.speed = 0.0f;
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

        if (nearestEnemy == null)
        {
            animator.SetBool("IsWalking", true);
            animator.SetBool("IsAttacking", false);
            return;
        }

        unit.transform.LookAt(nearestEnemy.transform);

        if (gun.ammoPerMagazine <= 0)
        {
            animator.SetBool("IsReloading", true);
            animator.SetBool("IsAttacking", false);
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
