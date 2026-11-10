using UnityEngine;
using UnityEngine.Events;

public class PickUp : MonoBehaviour
{
    
    [SerializeField] private int value = 1;
    
    public static PickUp instance;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Face"))
        {
            ScoreManager.instance.ChangeScore(value);
            Destroy(other.gameObject);
            Debug.Log("Destroyed");

            
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
