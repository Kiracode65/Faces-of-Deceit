using System;
using System.Collections.Generic;
using UnityEngine;

public enum FaceAttribute
{
    normal,
    damaged,
    young,
    old,
    pale,
    dark,
}
public class FaceObject : MonoBehaviour
{
    public storedFace FaceDetails;
    [SerializeField] private SpriteRenderer spriteRenderer;

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
        InventoryManager.Instance.PickupFace(this);
        dragging = true;
    }
    private void OnMouseUp()
    {
        dragging = false;
    }

    public void initialise(storedFace storedFace)
    {
        FaceDetails = storedFace;
        spriteRenderer.sprite = storedFace.faceScriptable.sprite;
    }
    public void StoreFace() // havent figured out how to do this yet, probably needs a shelf manager
    {
        ShelfLayout shelf = FindObjectOfType<ShelfLayout>();

        if(shelf != null)
        {
            shelf.RestoreFace(this);
        }
        else
        {
            InventoryManager.Instance.StoreFace(this);
        }

    }

    public void SellFace() // function here just in case we want the faces to do anything before dying, like an animation or smthn
    {
        Destroy(gameObject);
    }
}
