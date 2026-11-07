using UnityEngine;

public class DestroyStuff : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "NPC")
        {
            Destroy(gameObject);
            Debug.Log("Destroy");
        }
    }
}
