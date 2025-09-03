using UnityEngine;
using System.Collections.Generic;

public class FireExtinguisher : MonoBehaviour
{
    public ParticleSystem extinguisherParticles;
    public string fireTag = "Fire"; // Tag assigned to fire objects

    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    void Start()
    {
        if (extinguisherParticles == null)
        {
            extinguisherParticles = GetComponent<ParticleSystem>();
        }
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag(fireTag))
        {
            int numCollisionEvents = extinguisherParticles.GetCollisionEvents(other, collisionEvents);

            // Optional: Add minimum collision requirement
            if (numCollisionEvents > 0)
            {
                Destroy(other.gameObject);
            }
        }
    }
}