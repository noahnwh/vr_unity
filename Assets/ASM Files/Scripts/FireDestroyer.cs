using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fire"))
        {
            Fire f = other.GetComponent<Fire>();
            if (f != null)
                f.Extinguish();
            else
                Destroy(other.gameObject);
        }
    }
}
