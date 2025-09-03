using UnityEngine;

public class SprayTestDestroy : MonoBehaviour
{
    void OnParticleCollision(GameObject other)
    {
        Debug.Log($"{name} was hit by particles from {other.name}, destroying...");
        Destroy(gameObject);
    }
}
