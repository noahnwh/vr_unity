using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class LockFacingForwardWhileHeld : MonoBehaviour
{
    XRGrabInteractable grab;
    bool isHeld;
    Quaternion fixedOffset;  // rotation offset you want relative to forward

    void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();
        grab.selectEntered.AddListener(_ => { isHeld = true; });
        grab.selectExited.AddListener(_ => { isHeld = false; });
    }

    // Set this if your model’s forward isn’t already the spout
    // e.g. use (0,180,0) to flip if it points backwards
    public Vector3 eulerOffset = Vector3.zero;

    void LateUpdate()
    {
        if (!isHeld) return;

        // Face camera's yaw (no roll), so it acts like a CS gun
        Transform cam = Camera.main ? Camera.main.transform : null;
        if (!cam) return;

        // Take camera forward, remove pitch/roll ? keep only yaw
        Vector3 yawFwd = Vector3.ProjectOnPlane(cam.forward, Vector3.up).normalized;
        if (yawFwd.sqrMagnitude < 1e-6f) yawFwd = transform.forward;

        Quaternion target = Quaternion.LookRotation(yawFwd, Vector3.up) * Quaternion.Euler(eulerOffset);

        // Hard lock (no smoothing so it never drifts)
        transform.rotation = target;
    }
}