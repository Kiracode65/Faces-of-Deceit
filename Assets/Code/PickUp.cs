using UnityEngine;


public class PickUp : MonoBehaviour
{
   
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Face"))
        {
            Destroy(other.gameObject);
            
        }
    }
}
