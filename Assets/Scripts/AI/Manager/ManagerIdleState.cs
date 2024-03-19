using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ManagerIdleState : StateMachineBehaviour
{
    UnitManager unit;
    UnitList list;
    float startingHealth;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        unit = animator.GetComponentInParent<UnitManager>();
        list = GameObject.FindWithTag(unit.enemyTeamCount.ToString()).GetComponent<UnitList>();
        /*

        List<GameObject> unitPriority = list.unitsInATeam;

        unit.agent.ResetPath();

        unitPriority.Remove(unit.gameObject);
        unitPriority.Sort((u1,u2)=>u1.gameObject.GetComponentInParent<UnitManager>().health.CompareTo(u2.gameObject.GetComponentInParent<UnitManager>().health));

        unit.currentTarget = unitPriority[0];//GameObject.FindWithTag(unit.teamAffliation.ToString());
        */
        unit.currentTarget = list.unitsInATeam[Random.Range(0, list.unitsInATeam.Count)];
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (unit.health <= 0.0f)
        {
            animator.SetTrigger("Dead");
            return;
        }

        if (unit.currentTarget.activeInHierarchy == true)
        {
            animator.SetBool("IsWalking", true);
            animator.SetBool("IsIdle", false);
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
