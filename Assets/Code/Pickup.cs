using UnityEngine;

public class Pickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Face"))
        {
            Destroy(other.gameObject);
        }
       
    }
}
