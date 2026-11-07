using UnityEngine;

public class PickUp : MonoBehaviour
{
    public static PickUp instance;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Face"))
        {
            Destroy(other.gameObject);
            Debug.Log("Destroyed");

            ScoreManager.instance.AddPoint();
        }
    }
    void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
