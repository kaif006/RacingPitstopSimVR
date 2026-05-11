using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class F1WheelInteractable : XRGrabInteractable
{
    [Header("Dynamic Attach Points")]
    public Transform handAttachPoint;
    public Transform socketAttachPoint;

    protected override void Awake()
    {
        base.Awake();
        
        attachTransform = socketAttachPoint;
    }

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        if (!(args.interactorObject is XRSocketInteractor))
        {
            attachTransform = handAttachPoint;
        }
        else
        {
            attachTransform = socketAttachPoint;
        }

        base.OnSelectEntering(args);
    }

    protected override void OnSelectExiting(SelectExitEventArgs args)
    {
        base.OnSelectExiting(args);
        attachTransform = socketAttachPoint;
    }
}