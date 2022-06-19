using UnityEngine;

public class DestroyPlayerOnExit : StateMachineBehaviour {
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        Destroy(animator.gameObject.transform.parent.gameObject, stateInfo.length);
    }
}
