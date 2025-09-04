using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
[RequireComponent(typeof(AudioSource))]
public class FEParticleExtinguisher : MonoBehaviour
{
    [Tooltip("How fast the spray removes heat when it collides with fire.")]
    public float extinguishPerHit = 25f;

    private ParticleSystem part;
    private AudioSource audioSource;
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    void Start()
    {
        part = GetComponent<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (part.isPlaying)
        {
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else
        {
            if (audioSource.isPlaying)
                audioSource.Stop();
        }
    }

    void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);

        if (numCollisionEvents > 0)
        {
            var fire = other.GetComponentInParent<FireController>();
            if (fire != null)
            {
                fire.ApplyExtinguish(extinguishPerHit * numCollisionEvents);
            }
        }
    }
}


// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// [RequireComponent(typeof(ParticleSystem))]
// public class FEParticleExtinguisher : MonoBehaviour
// {
//     [Tooltip("How fast the spray removes heat when it collides with fire.")]
//     public float extinguishPerHit = 25f;
    
//     private ParticleSystem part;
//     private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

//     void Start()
//     {
//         part = GetComponent<ParticleSystem>();
//     }

//     void OnParticleCollision(GameObject other)
//     {
//         int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);
        
//         if (numCollisionEvents > 0)
//         {
//             // Look for FireController on the hit object or its parents
//             var fire = other.GetComponentInParent<FireController>();
//             if (fire != null)
//             {
//                 fire.ApplyExtinguish(extinguishPerHit * numCollisionEvents);
//             }
//         }
//     }
// }