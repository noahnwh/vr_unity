using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Rigidbody), typeof(Collider), typeof(XRGrabInteractable))]
public class PinPullable : MonoBehaviour
{
    public Transform seat; // optional: where the pin sits on the extinguisher

    Rigidbody rb;
    Collider col;
    XRGrabInteractable grab;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        grab = GetComponent<XRGrabInteractable>();

        // Seated state: stuck to extinguisher, no physics
        rb.isKinematic = true;
        rb.useGravity = false;
        col.isTrigger = true;

        grab.selectEntered.AddListener(OnGrabbed);
    }

    void OnGrabbed(SelectEnterEventArgs args)
    {
        // Detach and enable physics only when grabbed
        transform.SetParent(null, true);
        rb.isKinematic = false;
        rb.useGravity = true;
        col.isTrigger = false;

        // tiny nudge outwards (optional)
        if (seat != null)
        {
            Vector3 dir = (transform.position - seat.position).normalized;
            rb.AddForce(dir * 0.4f, ForceMode.Impulse);
        }
    }
}
