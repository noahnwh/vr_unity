using UnityEngine;

public class SprayTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fire"))
        {
            Fire fire = other.GetComponentInParent<Fire>();
            if (fire != null)
                fire.Extinguish();
            else
                Destroy(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Optional: keep killing fire if spray stays over it
        if (other.CompareTag("Fire"))
        {
            Destroy(other.gameObject);
        }
    }
}
