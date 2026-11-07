using UnityEngine;

public class Drag : MonoBehaviour
  
{
    public static Drag instance;
    private bool dragging = false;
    private Vector3 offset;
    private Vector3 baseOffset = new Vector3(0, 0, 11);
    
    void Update()
    {
        if (dragging)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(transform.position.x, transform.position.y, baseOffset.z);
            transform.position += offset;

        }
    }

    private void OnMouseDown()
    {
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragging = true;
    }
    private void OnMouseUp()
    {
        dragging = false;
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
