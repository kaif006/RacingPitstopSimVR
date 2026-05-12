using UnityEngine;

public class HideCarOnStart : StateMachineBehaviour
{
    // Runs as soon as the Animator enters the Waiting state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Find all Mesh Renderers on the car and its children and turn them off
        SetCarVisibility(animator.gameObject, false);
    }

    // Runs when we transition OUT of Waiting (to the Arrival state)
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Turn the car back on
        SetCarVisibility(animator.gameObject, true);
    }

    private void SetCarVisibility(GameObject obj, bool visible)
    {
        MeshRenderer[] renderers = obj.GetComponentsInChildren<MeshRenderer>();
        foreach (var r in renderers)
        {
            r.enabled = visible;
        }
    }
}