using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;

[RequireComponent(typeof(Rigidbody))]
public class ExtinguisherHMDLocked : XRGrabInteractable
{
    [Header("Explicit view pose under the extinguisher")]
    public Transform viewPose; // assign your ViewPose child

    public bool lockRoll = true;
    public float posLerp = 24f, rotLerp = 24f;

    Transform cam;
    Transform originalParent;
    Rigidbody rb;
    bool held;

    // cached local pose of ViewPose relative to extinguisher root
    Vector3 viewLocalPos;
    Quaternion viewLocalRot;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
        var xrOrigin = FindObjectOfType<XROrigin>();
        cam = xrOrigin ? xrOrigin.Camera.transform : Camera.main?.transform;

        if (!viewPose)
            Debug.LogWarning("Assign a ViewPose Transform (child of the extinguisher).");

        if (viewPose)
        {
            viewLocalPos = viewPose.localPosition;
            viewLocalRot = viewPose.localRotation;
        }

        movementType = MovementType.Kinematic;
        selectMode = InteractableSelectMode.Single; // << use this instead of maxInteractors
    }


    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        held = true;
        originalParent = transform.parent;

        rb.isKinematic = true;
        rb.useGravity = false;

        if (cam) transform.SetParent(cam, true);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        held = false;
        transform.SetParent(originalParent, true);
        rb.isKinematic = false;
        rb.useGravity = true;
    }

    void LateUpdate()
    {
        if (!held || cam == null || viewPose == null) return;

        // Place the extinguisher so that ViewPose matches camera forward
        Vector3 targetPos = cam.TransformPoint(viewLocalPos);
        Quaternion targetRot = cam.rotation * viewLocalRot;

        if (lockRoll)
        {
            // keep upright relative to world up
            Vector3 fwd = targetRot * Vector3.forward;
            Vector3 yawFwd = Vector3.ProjectOnPlane(fwd, Vector3.up).normalized;
            if (yawFwd.sqrMagnitude < 1e-6f) yawFwd = cam.forward;
            targetRot = Quaternion.LookRotation(yawFwd, Vector3.up) * viewLocalRot;
        }

        transform.position = Vector3.Lerp(transform.position, targetPos, posLerp * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotLerp * Time.deltaTime);
    }
}