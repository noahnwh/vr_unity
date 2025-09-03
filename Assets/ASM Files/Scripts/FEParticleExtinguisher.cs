using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attach to your white CO₂ ParticleSystem (FE Particle)
[RequireComponent(typeof(ParticleSystem))]
public class FEParticleExtinguisher : MonoBehaviour
{

    private void OnParticleCollision(GameObject other)
    {
        // Simple test: destroy spheres
        if (other.CompareTag("Fire"))   // or remove this line to destroy anything hit
        {
            Destroy(other);
        }
    }
    // [Tooltip("How fast the spray removes heat per collision when using FireController.")]
    // public float extinguishPerHit = 25f;

    // [Tooltip("Optional: only interact with objects on this tag. Leave empty to ignore tag checks.")]
    // public string fireTag = ""; // e.g. "Fire"

    // public ParticleSystem extinguisherParticles;

    // void Start()
    // {
    //     if (extinguisherParticles == null)
    //     {
    //         extinguisherParticles = GetComponent<ParticleSystem>();
    //     }
    // }

    // void OnParticleCollision(GameObject other)
    // {
    //     if (!string.IsNullOrEmpty(fireTag) && !other.CompareTag(fireTag))
    //     {
    //         return; // filtering by tag if provided
    //     }

    //     // Prefer gradual extinguish via FireController if present
    //     var fireController = other.GetComponentInParent<FireController>();
    //     if (fireController != null)
    //     {
    //         fireController.ApplyExtinguish(extinguishPerHit);
    //         return;
    //     }

    //     // Fallback: support simple Fire.cs which extinguishes immediately
    //     var simpleFire = other.GetComponentInParent<Fire>();
    //     if (simpleFire != null)
    //     {
    //         simpleFire.Extinguish();
    //         return;
    //     }
    // }
}