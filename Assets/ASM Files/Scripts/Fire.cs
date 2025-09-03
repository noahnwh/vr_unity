using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public ParticleSystem fireParticles;

    public void Extinguish()
    {
        Destroy(gameObject);
        // if (fireParticles != null)
        //     fireParticles.Stop();

        // Destroy(gameObject, 1f); // wait 1 sec so flames fade out
    }
}
