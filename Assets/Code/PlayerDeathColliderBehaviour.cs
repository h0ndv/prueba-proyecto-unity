using UnityEngine;

public class PlayerDeathColliderBehaviour : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var collider = animator.GetComponent<BoxCollider2D>();
        if (collider != null)
            collider.enabled = false;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var collider = animator.GetComponent<BoxCollider2D>();
        if (collider != null)
            collider.enabled = true;
    }
}
