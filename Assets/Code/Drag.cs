using UnityEngine;

public class Drag : MonoBehaviour{
    private bool dragging = false;
    private Vector3 offset;
    public static Drag instance;



    private void Update() {
        
        if (dragging) {

            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        }


    }



    private void OnMouseDown() {
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);

        dragging = true;

    }


    private void OnMouseUp() {


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





