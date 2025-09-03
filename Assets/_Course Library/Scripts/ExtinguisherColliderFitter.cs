using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ExtinguisherColliderFitter.cs

[RequireComponent(typeof(MeshRenderer))]
public class ExtinguisherColliderFitter : MonoBehaviour
{
    [Tooltip("Assign an existing CapsuleCollider or leave empty to auto-add on this GameObject.")]
    public CapsuleCollider targetCapsule;
    [Range(0.7f, 1.0f)] public float bodyTightness = 0.9f; // shrink to avoid snagging

    void Reset() { Fit(); }
    void OnValidate() { if (Application.isEditor && !Application.isPlaying) Fit(); }

    public void Fit()
    {
        var mr = GetComponent<MeshRenderer>();
        if (!mr) return;

        if (!targetCapsule) targetCapsule = gameObject.GetComponent<CapsuleCollider>()
                                  ?? gameObject.AddComponent<CapsuleCollider>();

        // Use local-space bounds to avoid scale/rotation issues
        var b = mr.localBounds;
        var centerLocal = b.center;
        var sizeLocal = b.size;

        // Assume bottle stands upright in local Y
        targetCapsule.direction = 1; // 0=X, 1=Y, 2=Z
        targetCapsule.center = centerLocal;

        // Use X/Z as diameter, Y as height
        float radius = Mathf.Min(Mathf.Abs(sizeLocal.x), Mathf.Abs(sizeLocal.z)) * 0.5f * bodyTightness;
        float height = Mathf.Abs(sizeLocal.y) * bodyTightness;

        // Capsule height must be >= 2*radius
        height = Mathf.Max(height, radius * 2f + 0.001f);

        targetCapsule.radius = radius;
        targetCapsule.height = height;
        targetCapsule.isTrigger = false;
    }
}
