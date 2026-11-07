using UnityEngine;

public class DestroyStuff : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "NPC")
        {
            Destroy(gameObject);
            Debug.Log("Destroy");
        }
    }
}
