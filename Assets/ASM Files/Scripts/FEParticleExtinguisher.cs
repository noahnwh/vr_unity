using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class FEParticleExtinguisher : MonoBehaviour
{
    [Tooltip("How fast the spray removes heat when it collides with fire.")]
    public float extinguishPerHit = 25f;
    
    private ParticleSystem part;
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    void Start()
    {
        part = GetComponent<ParticleSystem>();
    }

    void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);
        
        if (numCollisionEvents > 0)
        {
            // Look for FireController on the hit object or its parents
            var fire = other.GetComponentInParent<FireController>();
            if (fire != null)
            {
                fire.ApplyExtinguish(extinguishPerHit * numCollisionEvents);
            }
        }
    }
}



// // Attach to your white CO₂ ParticleSystem (FE Particle)
// [RequireComponent(typeof(ParticleSystem))]
// public class FEParticleExtinguisher : MonoBehaviour
// {
//     private void OnParticleCollision(GameObject other)
//     {
//         Destroy(other);
//     }

//     // private void OnParticleCollision(GameObject other)
//     // {
//     //     // Simple test: destroy spheres
//     //     if (other.CompareTag("Fire"))   // or remove this line to destroy anything hit
//     //     {
//     //         Destroy(other);
//     //     }
//     // }
//     // [Tooltip("How fast the spray removes heat per collision when using FireController.")]
//     // public float extinguishPerHit = 25f;

//     // [Tooltip("Optional: only interact with objects on this tag. Leave empty to ignore tag checks.")]
//     // public string fireTag = ""; // e.g. "Fire"

//     // public ParticleSystem extinguisherParticles;

//     // void Start()
//     // {
//     //     if (extinguisherParticles == null)
//     //     {
//     //         extinguisherParticles = GetComponent<ParticleSystem>();
//     //     }
//     // }

//     // void OnParticleCollision(GameObject other)
//     // {
//     //     if (!string.IsNullOrEmpty(fireTag) && !other.CompareTag(fireTag))
//     //     {
//     //         return; // filtering by tag if provided
//     //     }

//     //     // Prefer gradual extinguish via FireController if present
//     //     var fireController = other.GetComponentInParent<FireController>();
//     //     if (fireController != null)
//     //     {
//     //         fireController.ApplyExtinguish(extinguishPerHit);
//     //         return;
//     //     }

//     //     // Fallback: support simple Fire.cs which extinguishes immediately
//     //     var simpleFire = other.GetComponentInParent<Fire>();
//     //     if (simpleFire != null)
//     //     {
//     //         simpleFire.Extinguish();
//     //         return;
//     //     }
//     // }
// }