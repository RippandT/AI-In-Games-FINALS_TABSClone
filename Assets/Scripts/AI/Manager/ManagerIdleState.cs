using System.Threading.Tasks;
using UnityEngine;

public class ManagerIdleState : StateMachineBehaviour
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

        GameObject nearestAlly = GameObject.FindWithTag(unit.teamAffliation.ToString());

        if (nearestAlly != null && nearestAlly != unit.gameObject)
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
